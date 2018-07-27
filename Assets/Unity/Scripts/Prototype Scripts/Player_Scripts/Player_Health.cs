using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified from tutorial by BurgZerg Arcade: https://www.youtube.com/watch?v=YYqzz1dy3Ak&list=PLE5C2870574BF4B06&index=1

public class Player_Health : MonoBehaviour {

	public int maxHealth = 100;
	public int curHealth = 100;

	public float healthBarLength;
	public GUISkin skin;

	private Vector3 origPos;

	private Camera_Manager myOtherCameraManager;
	private Camera_Trigger myCameraTrigger;

	// Use this for initialization
	void Start () {
		healthBarLength = Screen.width / 3;
		origPos = transform.position; // Marks Starting Position
		// Calls in camera functions to this script
		myOtherCameraManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Camera_Manager> ();
		myCameraTrigger = GameObject.FindGameObjectWithTag ("camCollider").GetComponent<Camera_Trigger> ();
	}
	
	// Update is called once per frame
	void Update () {
		AdjustCurrentHealth (0);
	}

	void OnGUI(){
		
		GUI.skin = skin;

		int x = Screen.height/10;

		GUI.Box (new Rect (10, x, healthBarLength, Screen.height/30),"", skin.GetStyle("Health_Bar")); // Bar that depletes
		GUI.Box (new Rect (10, x, Screen.width/3, Screen.height/30), "", skin.GetStyle("Health")); // 'Container' for above
	}

	public void AdjustCurrentHealth(int adj){
		curHealth += adj;

		if (curHealth < 0) {
			curHealth = 0;
			// Reset Player
			transform.position = origPos; // Unable to restore rotation
			// Restore Health
			curHealth = maxHealth;
			// Reset Camera
			myCameraTrigger.cameraToMoveTo.SetActive (false); // Deactivates Death Camera
			myOtherCameraManager.startCamera.SetActive (true); // Activates Spawn camera
		}
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		if (maxHealth < 1) {
			maxHealth = 1;
		}

		healthBarLength = (Screen.width / 3) * (curHealth / (float)maxHealth); // Location and shape of health bar
	}
}