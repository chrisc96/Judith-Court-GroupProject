using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Range : MonoBehaviour {

    //[HideInInspector]
	public bool withinRange = false;

	// Determines whether the gameobject is within range of the player
	void OnTriggerEnter (Collider coll) {
        if (coll.CompareTag("Player")) {
            if (gameObject.activeInHierarchy) {
                withinRange = true;
            }
            else {
                withinRange = false;
            }
        }
	}

	void OnTriggerExit (Collider coll){
        if(coll.CompareTag("Player")) {
            withinRange = false;
        }
	}
}