using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified from tutorial by BurgZerg Arcade: https://www.youtube.com/watch?v=fxSM_INrmF8

public class Player_Attack : MonoBehaviour {

	public GameObject target;
	public float attackTimer; // Adds slight hit delay
	public float coolDown;
	public Animator anim;

	private Movement movement;
	private Show_Weapon equipped;

	bool moveTimer = false;

	void Start () {
		attackTimer = 0.4f;
		coolDown = 1f;
		anim = GetComponent<Animator> ();
		movement = GameObject.FindGameObjectWithTag ("Player").GetComponent<Movement> ();
		equipped = GameObject.FindGameObjectWithTag ("Player").GetComponent<Show_Weapon> ();
	}

	void Update () {

		if (attackTimer > 0) {
			attackTimer -= Time.deltaTime;
		}

		if (attackTimer < 0) { 
			attackTimer = 0;
		}

		if (Input.GetKeyDown (KeyCode.F)) {
			StartCoroutine (hitSync ());
			this.hitSync ();
			anim.Play ("Swing Attack", -1, 0f); // plays animation
			moveTimer = true; 
			StartCoroutine (pausePos ());
			this.pausePos ();
		} 
	}

	// Timers

	IEnumerator pausePos() { // Freezes the player position for the duration of the attack
		if (moveTimer == true) {
			movement.velocity = 0;
			movement.rotationSpeed = 0;
		}
		yield return new WaitForSeconds(1.5f);
		moveTimer = false;
		movement.velocity = 2;
		movement.rotationSpeed = 100;
	}

	IEnumerator hitSync(){ // Syncs damage dealt with animation
		yield return new WaitForSeconds (1.2f);
		Attack ();
	}

	// Attack

	private void Attack(){
		float distance = Vector3.Distance (target.transform.position, transform.position);

		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot (dir, transform.forward);

		if (distance < 2) { // If the player is facing towards the enemy and is within a certain distance of them, they can deal damage
			if (direction > 0) {
				Enemy_Health eh = (Enemy_Health) target.GetComponent ("Enemy_Health");
				eh.AdjustCurrentHealth (-5);
				if (equipped.showWeapon) { // If the baseball bat is equipped the player will deal twice as much damage
					eh.AdjustCurrentHealth (-10);
				}
			}
		}
	}
}