using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified from tutorial by BurgZerg Arcade: https://www.youtube.com/watch?v=YYqzz1dy3Ak&list=PLE5C2870574BF4B06&index=1

public class Enemy_AI : MonoBehaviour {

	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
	public int maxDistance;
	public int minDistance;
	public Animator anim;

	private Transform myTransform;

	void Awake(){
		myTransform = transform;
		anim = GetComponent<Animator> ();
	}
		
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag ("Player");

		target = go.transform;

		maxDistance = 1; // Max and min distance the player has to be within for the enemy to start pursuing them
		minDistance = 9;
	}

	void Update () {
		if (Vector3.Distance (target.position, myTransform.position) > maxDistance && Vector3.Distance (target.position, myTransform.position) < minDistance) {
			// Move towards target
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
			// Look at target
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position - myTransform.position), rotationSpeed * Time.deltaTime);
			anim.SetBool ("IsWalking", true);
		} else {
			anim.SetBool ("IsWalking", false);
		}
	}
}
