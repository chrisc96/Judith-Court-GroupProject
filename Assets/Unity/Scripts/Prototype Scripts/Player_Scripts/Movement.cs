using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adapted from code by CB Showtunes on youtube: https://www.youtube.com/watch?v=HgW-rAxnEqc

public class Movement : MonoBehaviour {

	public float velocity;
	public float rotationSpeed;

	static Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float translation = Input.GetAxis ("Vertical")* velocity;
		float rotation = Input.GetAxis ("Horizontal")* rotationSpeed;
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;

		transform.Translate(0, 0, translation);
		transform.Rotate(0, rotation, 0);

		// Walking animation

		if (translation != 0){
			anim.SetBool("IsWalking", true);
		} else {
			anim.SetBool("IsWalking", false);
		}

		// Running

		//if (Input.GetKeyDown (KeyCode.LeftShift)) {
		//	speed = speed * 2;
		//	anim.SetBool ("IsRunning", true);
		//} else if (Input.GetKeyUp(KeyCode.LeftShift)){
		//	speed = 2;
		//	anim.SetBool ("IsRunning", false);
		//}
	}
}
