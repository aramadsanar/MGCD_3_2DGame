using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementScript : MonoBehaviour {

	public float speed = 5f;
	public float jumpSpeed = 5f;
	public float movement = 0f;
	public Rigidbody2D rb;
	public int score = 0;
	bool moveAllowed = false;
	float dy = 0.0f, dx = 0.0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		
	}

	bool stringStartsWith(string input, string pattern) {
		for (int i = 0; i < (pattern.Length); i++) {
			if (input [i] != pattern [i])
				return false;
		}
		return true;
	}
	void OnCollisionEnter2D(Collision2D c) {
		Debug.Log (c.gameObject.name);
		//"CoinSprite"
		if (stringStartsWith(c.gameObject.name, "CoinSprite")) {
			score++;
			Destroy (c.gameObject);
			Debug.Log ("Current Score: " + score);
		}
		
	}
	// Update is called once per frame
	void Update () {
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

		/*movement = Input.GetAxis ("Vertical");
		//Debug.Log ("Movement: " + movement + "\n");
		if (movement >0f || movement <0f)
			rb.velocity = new Vector2 (rb.velocity.x, movement * speed);
		else 
			rb.velocity = new Vector2 (rb.velocity.x, 0);

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
		//	rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
	}*/
}
