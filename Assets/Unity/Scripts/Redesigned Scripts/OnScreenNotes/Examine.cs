using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examine : MonoBehaviour {

	public Transform ExamineObject; // "Examine - E" text
	public Transform examine; // Object to examine

    [HideInInspector]
    public bool canView = false;
    [HideInInspector]
    public bool isViewing = false;

	void OnTriggerStay (Collider coll) {
        if(coll.CompareTag("Player")) {
            canView = true;
		    if (!ExamineObject.gameObject.activeInHierarchy) {
			    ExamineObject.gameObject.SetActive (true);
		    }
        }
	}

	void OnTriggerExit (Collider coll){
        if(coll.CompareTag("Player")) {
            canView = false;
		    if (ExamineObject.gameObject.activeSelf) {
			    ExamineObject.gameObject.SetActive (false);
		    }
        }
	}

	void Update () {
		if (Input.GetKeyDown (Game_Manager.interact) && canView && !isViewing){
			if (!examine.gameObject.activeSelf){
				examine.gameObject.SetActive (true);
				ExamineObject.gameObject.SetActive (false);
                isViewing = true;
                Time.timeScale = 0;
            }
		}
        else if (isViewing && (Input.GetKeyDown(Game_Manager.interact) || Input.GetKeyDown(KeyCode.Escape))) {
            Time.timeScale = 1;
            examine.gameObject.SetActive(false);
            ExamineObject.gameObject.SetActive(true);
            isViewing = false;
        }
    }
}
