using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour 
{
	//private Rigidbody2D rb2d;

	[Range(1f, 20f)]
	float scrollSpeed = 1f;
	Vector2 startPos;

	float newPos;
	// Use this for initialization
	void Start () 
	{
		Debug.Log ("Scroll module init\n");
		startPos = transform.position;

		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		//rb2d = GetComponent<Rigidbody2D>();

		//Start the object moving.
		//rb2d.velocity = new Vector2 (-1.5f, 0);
	}

	void Update()
	{
		Debug.Log ("update called\n");

		// If the game is over, stop scrolling.
		if(MovementScript.instance.gameOver == false)
		{
			//rb2d.velocity = Vector2.zero;
			float newPos = Mathf.Repeat (Time.time * -scrollSpeed, 20);
			transform.position = startPos + Vector2.right * newPos;
		}
	}
}