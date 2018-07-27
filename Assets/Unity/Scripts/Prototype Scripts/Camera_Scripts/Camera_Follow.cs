using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {

	public Transform nonPlayerTarget; // Attach what you want the camera/scriptholder to follow in the inspector

    private void Start() {
        if(nonPlayerTarget == null) {
            nonPlayerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update () {
		transform.LookAt(nonPlayerTarget);
	}
}