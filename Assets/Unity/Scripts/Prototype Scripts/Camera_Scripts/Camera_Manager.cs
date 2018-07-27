using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour {

	public GameObject[] cameras;
	public GameObject startCamera;

	// Use this for initialization
	void Start () {
		cameras = GameObject.FindGameObjectsWithTag ("Camera");

		for (int i = 0; i < cameras.Length; i++) {
			// Turns off all cameras in the scene
			cameras [i].SetActive (false);
		}
		startCamera.SetActive (true);
		// Sets camera to be looking 
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Controller> ().cameraRespawnLookingAtPlayer = startCamera;
	}

	public void DeactivateAllCameras(){
		for (int i = 0; i < cameras.Length; i++) {
			// Turns off all cameras in the scene
			cameras [i].SetActive (false);
		}
	}
}