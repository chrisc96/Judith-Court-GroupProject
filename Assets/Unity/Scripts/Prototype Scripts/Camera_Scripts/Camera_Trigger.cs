using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Trigger : MonoBehaviour {

	public GameObject cameraToMoveTo; // Choose camera to be changed to
	private Camera_Manager myCameraManager;

	// Use this for initialization
	void Start () {
		myCameraManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Camera_Manager> ();
	}

	void OnTriggerEnter (Collider other) { // Change camera to selection
		if (other.CompareTag ("Player")) {
			myCameraManager.DeactivateAllCameras();
			cameraToMoveTo.SetActive (true);
		}
	}
}