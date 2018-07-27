using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified from tutorial by BurgZerg Arcade: https://www.youtube.com/watch?v=YYqzz1dy3Ak&list=PLE5C2870574BF4B06&index=1

public class Enemy_Attack : MonoBehaviour {

	public GameObject target;
	public float attackTimer; // Adds slight hit delay
	public float coolDown;

	// Use this for initialization
	void Start () {
		attackTimer = 0;
		coolDown = 3.0f;
	}

	// Update is called once per frame
	void Update () {
		if (attackTimer > 0) {
			attackTimer -= Time.deltaTime;
		}

		if (attackTimer < 0) { 
			attackTimer = 0;
		}

		if (attackTimer == 0) {
			Attack();
			attackTimer = coolDown;
		}		
	}

	private void Attack(){ // Enemy has to be facing player and within certain distance to deal damage
		float distance = Vector3.Distance (target.transform.position, transform.position);

		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot (dir, transform.forward);

		if (distance < 2) {
			if (direction > 0) {
				Player_Health ph = (Player_Health)target.GetComponent ("Player_Health");
				ph.AdjustCurrentHealth (-10);
			}
		}
	}
}
