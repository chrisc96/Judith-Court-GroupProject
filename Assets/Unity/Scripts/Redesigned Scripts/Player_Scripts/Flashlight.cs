using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    [HideInInspector]
	public bool isOn = false;

	public GameObject flashLight;
	public GameObject otherLight;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (Game_Manager.lightToggle)) {
			isOn = !isOn;
		}

		if (isOn) {
			flashLight.SetActive (true);
			otherLight.SetActive (true);
		}
		if (!isOn) {
			flashLight.SetActive (false);
			otherLight.SetActive (false);
		}
	}
}