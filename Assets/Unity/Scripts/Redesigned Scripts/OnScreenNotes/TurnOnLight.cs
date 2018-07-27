using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOnLight : MonoBehaviour {

	public Transform flashlight_note;
	public Transform Initial_note;
	public Transform turnOn_note;
	public Transform toggle_note;
    public Image blackBGImg;

    Boolean isInTransition = false;

	public bool play = false;
	public bool unableToPlay = false;


    void OnTriggerEnter (Collider coll) {
		if (unableToPlay == true) {
			play = false;
		}
        else {
			play = true;
			Time.timeScale = 0;
            // The problem is fade manager now used unscaled time. So interrupting fades with Time.timeScale = 0 won't work anymore

			if (play == true) {
                blackBGImg.color = new Color(0, 0, 0, 1);
                Initial_note.gameObject.SetActive (true);
            }
		}
	}


	void Update () {
		if (Input.GetKeyDown (Game_Manager.interact) && Initial_note.gameObject.activeInHierarchy == true) {	
			turnOn_note.gameObject.SetActive (true);
			Initial_note.gameObject.SetActive (false);
		}

		else if (Input.GetKeyDown (Game_Manager.interact) && turnOn_note.gameObject.activeInHierarchy == true) {
			toggle_note.gameObject.SetActive (true);
			turnOn_note.gameObject.SetActive (false);
        }

		else if (Input.GetKeyDown (Game_Manager.lightToggle) && toggle_note.gameObject.activeInHierarchy == true) {
            toggle_note.gameObject.SetActive (false);
            isInTransition = true;
            unableToPlay = true;
			Time.timeScale = 1;
		}

        if (isInTransition) {
            StartCoroutine(fadeToClear(0.75f));
        }
	}

    private IEnumerator fadeToClear(float v) {
        blackBGImg.color = new Color(blackBGImg.color.r, blackBGImg.color.g, blackBGImg.color.b, blackBGImg.color.a - (Time.unscaledDeltaTime * (1 / v)));
        if (blackBGImg.color.a <= 0) {
            isInTransition = false;
        }
        yield return null;
    }
}