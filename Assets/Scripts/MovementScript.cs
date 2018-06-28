using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class MovementScript : MonoBehaviour {

	public static MovementScript instance = new MovementScript();
	public float speed = 5f;
	public float jumpSpeed = 5f;
	public float movement = 0f;
	public Rigidbody2D rb;
	public int score = 0;
	public Text scoreText;
	//Native touch driver's
	bool moveAllowed = false;
	//public GameObject[] arrayOfCoins = new GameObject[10240];
	float dy = 0.0f, dx = 0.0f;
	public Transform coinPrefab;
	public Transform opponentPrefab;
	private IEnumerator coroutine;
	private bool isCrippled = false;
	private int count = 0;
	public float scrollSpeed = -1.5f;
	public bool gameOver = false;
	private AudioSource audioSource;
	public AudioClip audioClipOnEating;
	public AudioClip audioClipOnDeath;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		scoreText = GameObject.Find ("Text").GetComponent<Text> ();
		coroutine = WaitAndSpawn (0.7f);
		StartCoroutine (coroutine);
		audioSource = gameObject.GetComponent<AudioSource> ();
	}

	private IEnumerator WaitAndSpawn(float waitTime)
	{
		while (true)
		{
			yield return new WaitForSeconds(waitTime);
			print("WaitAndPrint " + Time.time);
			if (count % 3 == 0) {
				Transform temp = Instantiate (opponentPrefab, new Vector3 (10.0f, UnityEngine.Random.Range (-2.0f, 2.0f), 0), Quaternion.identity);
				Rigidbody2D rbtemp = temp.GetComponent<Rigidbody2D> ();
				rbtemp.velocity = new Vector2 (-2.0f, 0.0f);
			} else {
				Transform temp = Instantiate (coinPrefab, new Vector3 (10.0f, UnityEngine.Random.Range (-2.0f, 2.0f), 0), Quaternion.identity);
				Rigidbody2D rbtemp = temp.GetComponent<Rigidbody2D> ();
				rbtemp.velocity = new Vector2 (-2.0f, 0.0f);
			}

			count++;
		}
	}

	//I can't find the startsWith function built into Unity, so I made it myself..
	bool stringStartsWith(string input, string pattern) {
		for (int i = 0; i < (pattern.Length); i++) {
			if (input [i] != pattern [i])
				return false;
		}
		return true;
	}

	void OnCollisionEnter2D(Collision2D c) {
		//Diagnosis
		Debug.Log (c.gameObject.name);

		//TODO: Add support for real object detection, such as vegetables, junk foods and bonus packs, anyone?

		if (!instance.gameOver) {
			if (stringStartsWith (c.gameObject.name, "CoinSprite")) {
				Destroy (c.gameObject);

				//TODO: Add score printing element on screen!
				score++;
				Debug.Log ("Current Score: " + score);
				scoreText.text = ("Score: " + score);



				//audioSource.clip = audioClipOnEating;
				audioSource.PlayOneShot(audioClipOnEating, 1.0F);
				audioSource.PlayOneShot(audioClipOnDeath, 1.0F);
			} else if (stringStartsWith (c.gameObject.name, "Opponent")) {
				Debug.Log ("Opponent Object touched");
				//score--;
				//if (score < 0) {
				//game over here.
				scoreText.text = "GAME OVER";
				isCrippled = true;
				StopCoroutine (coroutine);
				rb.velocity = new Vector2 (0.0f, 0.0f);
				instance.gameOver = true;
				//}
			}
		}
	}

	void keyboardParseInput() {
		movement = Input.GetAxis ("Vertical");
		//Debug.Log ("Movement: " + movement + "\n");
		if (movement >0f || movement <0f)
			rb.velocity = new Vector2 (rb.velocity.x, movement * speed);
		else 
			rb.velocity = new Vector2 (rb.velocity.x, 0);
		
	}

	void mouseAndTouchParseInput() {
		//Gets the world position of the mouse on the screen        
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );

		//Checks whether the mouse is over the sprite
		bool overSprite = this.GetComponent<SpriteRenderer>().bounds.Contains( mousePosition );

		//If it's over the sprite
		if (overSprite)
		{
			//If we've pressed down on the mouse (or touched on the iphone)
			if (Input.GetButton("Fire1"))
			{
				//Set the position to the mouse position
				/*Input.mousePosition).x*/
				this.transform.position = new Vector2(rb.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
			}
		}
	}

	// Update is called once per frame
	void Update () {
//		Debug.Log ();
		//Keyboard driver. Geeks love keyboards, right? :)
		if (isCrippled == false) {
			keyboardParseInput ();

			//Touch driver: through mouse emulation
			mouseAndTouchParseInput ();
		}
	}
}
