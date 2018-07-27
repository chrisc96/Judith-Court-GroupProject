using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowbarSnapped : MonoBehaviour {

	public Transform Crowbar_message;
	public Transform broken;
	public Transform OhWell;

	public bool play = false;
	public bool unableToPlay = false;

	void OnTriggerEnter (Collider coll) {
		if (unableToPlay == true) {
			play = false;
		} 
		else {
			play = true;
			Time.timeScale = 0;

			if (Crowbar_message.gameObject.activeInHierarchy == false && play == true) {
				Crowbar_message.gameObject.SetActive (true);
				broken.gameObject.SetActive (true);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (Game_Manager.interact) && broken.gameObject.activeInHierarchy == true) {	
			OhWell.gameObject.SetActive (true);
			broken.gameObject.SetActive (false);
		}

		else if (Input.GetKeyDown (Game_Manager.interact) && OhWell.gameObject.activeInHierarchy == true) {
			OhWell.gameObject.SetActive (false);
			Crowbar_message.gameObject.SetActive (false);
			unableToPlay = true;
			Time.timeScale = 1;
		}
	}
}
