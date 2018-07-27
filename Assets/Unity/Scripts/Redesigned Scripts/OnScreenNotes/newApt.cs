using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newApt : MonoBehaviour {

	public Transform myRoom;
	public Transform Note_01;
	public Transform Note_02;

	public bool canView = false;

	void OnTriggerEnter (Collider coll) {
		canView = true;
	}

	void OnTriggerExit (Collider coll){
		canView = false;
	}

	void Update () {
		if (Input.GetKeyDown (Game_Manager.interact) && canView == true){
			
			if (myRoom.gameObject.activeInHierarchy == false) {
				myRoom.gameObject.SetActive (true);
				Note_01.gameObject.SetActive (true);
				Time.timeScale = 0;
			} 
            else if (/*Input.GetKeyDown (Game_Manager.interact) &&*/ Note_01.gameObject.activeInHierarchy == true){
				Note_02.gameObject.SetActive (true);
				Note_01.gameObject.SetActive (false);
			}
            else if (/*Input.GetKeyDown (Game_Manager.interact) &&*/ Note_02.gameObject.activeInHierarchy == true) {
				Note_02.gameObject.SetActive (false);
				myRoom.gameObject.SetActive (false);
				Time.timeScale = 1;
			}

		}
	}
}
