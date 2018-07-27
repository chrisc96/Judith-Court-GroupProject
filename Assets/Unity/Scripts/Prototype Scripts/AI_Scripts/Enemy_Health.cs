using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified from tutorial by BurgZerg Arcade: https://www.youtube.com/watch?v=YYqzz1dy3Ak&list=PLE5C2870574BF4B06&index=1

public class Enemy_Health : MonoBehaviour {

	public int maxHealth = 100;
	public int curHealth = 100;

	public GUISkin skin;

	public float healthBarLength;

	// Use this for initialization
	void Start () {
		healthBarLength = Screen.width / 3;
	}

	// Update is called once per frame
	void Update () {
		AdjustCurrentHealth (0);
	}

	void OnGUI(){ // Remove this for final game, player shouldn't know how much health enemy has
		GUI.skin = skin;

		int x = Screen.height/50*7;

		GUI.Box (new Rect (10, x, healthBarLength, Screen.height/30), "", skin.GetStyle("Enemy_Health_Bar")); // If you want text indicator: curHealth + "/" + maxHealth
		GUI.Box (new Rect (10, x, Screen.width /3, Screen.height/30), "", skin.GetStyle("Enemy_Health")); // Container for bar that depletes
	}

	public void AdjustCurrentHealth(int adj){
		curHealth += adj;

		if (curHealth < 0) {
			curHealth = 0;
			// Destroy enemy after health fully depleted
			Destroy (this.gameObject);
		}
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		if (maxHealth < 1) {
			maxHealth = 1;
		}

		healthBarLength = (Screen.width / 3) * (curHealth / (float)maxHealth);
	}
}