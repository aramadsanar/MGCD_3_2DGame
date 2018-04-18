using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MovementScript : MonoBehaviour {

	public float speed = 5f;
	public float jumpSpeed = 5f;
	public float movement = 0f;
	public Rigidbody2D rb;
	public int score = 0;
	public Text scoreText;
	//Native touch driver's
	bool moveAllowed = false;
	float dy = 0.0f, dx = 0.0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		scoreText = GameObject.Find ("Text").GetComponent<Text> ();
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
		if (stringStartsWith(c.gameObject.name, "CoinSprite")) {
			Destroy (c.gameObject);

			//TODO: Add score printing element on screen!
			score++;
			Debug.Log ("Current Score: " + score);
			scoreText.text = ("Score: " + score);
		}
	}

	// Update is called once per frame
	void Update () {

		//Keyboard driver. Geeks love keyboards, right? :)
		movement = Input.GetAxis ("Vertical");
		//Debug.Log ("Movement: " + movement + "\n");
		if (movement >0f || movement <0f)
			rb.velocity = new Vector2 (rb.velocity.x, movement * speed);
		else 
			rb.velocity = new Vector2 (rb.velocity.x, 0);


		//Touch driver: through mouse emulation
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

		//Disabled native touch driver as it feels clunky, if anyone could help with this, please do so :)
		/*
		if (Input.touchCount > 0) {
			Touch t = Input.GetTouch (0);
			Vector2 touchPos = Camera.main.ScreenToWorldPoint (t.position);
			switch (t.phase) {
			case TouchPhase.Began:
				if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos)) {
					//dx = touchPos.x - transform.position.x;
					//dy = 0.0f;
					dy = touchPos.y - transform.position.y;

					// if touch begins within the ball collider
					// then it is allowed to move
					moveAllowed = true;
				}
				break;
			case TouchPhase.Moved:
				if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos) && moveAllowed) {
					rb.MovePosition (new Vector2 (0, touchPos.y - dy));
					//rb.MovePosition (new Vector2 (0, Input.GetTouch(0).position.y));
				}
				break;
			case TouchPhase.Ended:
				moveAllowed = false;
				break;
			}
		}

		//if (Input.GetButtonDown("Jump"))
		//	rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);*/
	}
}
