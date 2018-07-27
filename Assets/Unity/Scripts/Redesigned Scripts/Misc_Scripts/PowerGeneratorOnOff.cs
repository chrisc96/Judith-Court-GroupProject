using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGeneratorOnOff : MonoBehaviour {

	public GameObject Off;
	public GameObject On;
	public GameObject LeverOff;
	public GameObject LeverOn;

	public bool isOff;
	public bool isOn;

	private Item_Range inRange;

	// The weapon is always in the players hand, it just need to be activated by 'picking up' the weapon on the ground.
	void Start () {
		isOff = true;
		isOn = false;

		inRange = GameObject.Find("Off").GetComponent<Item_Range> ();
	}

	void Update () {
		if (isOff == true) {
			On.SetActive (false);
			Off.SetActive (true);
			LeverOn.SetActive (false);
			LeverOff.SetActive (true);
		}
		if (isOff == false) {
			On.SetActive (true);
			Off.SetActive (false);
			LeverOn.SetActive (true);
			LeverOff.SetActive (false);
		}
		if (Input.GetKeyDown (Game_Manager.interact) && isOff == true && inRange.withinRange) {
			isOff = false;
			isOn = true;
		}
	}
}
