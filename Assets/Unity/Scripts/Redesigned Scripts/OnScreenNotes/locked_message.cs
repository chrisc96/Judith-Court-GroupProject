using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locked_message : MonoBehaviour {

	public Transform Door_Locked;
	public bool canView = false;

	void OnTriggerStay (Collider coll) {
        if (coll.CompareTag("Player") || coll.CompareTag("Weapon")) {
            canView = true;
        }
	}

	void OnTriggerExit (Collider coll){
        if(coll.CompareTag("Player") || coll.CompareTag("Weapon")) {
            canView = false;
        }
	}

	void Update () {
		if (Input.GetKeyDown (Game_Manager.interact) && canView){
			if (!Door_Locked.gameObject.activeInHierarchy){
				Door_Locked.gameObject.SetActive (true);
				Time.timeScale = 0;
			} 
            else {
				Door_Locked.gameObject.SetActive (false);
				Time.timeScale = 1;
			}
		}
	}
}
