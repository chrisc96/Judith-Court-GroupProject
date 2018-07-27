using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script adapted from gamesplusjames on Youtube: https://www.youtube.com/watch?v=XoH8Qyqje1g

public class Audio_Collider : MonoBehaviour {

	public AudioClip newTrack;

	private Audio_Manager theAM;

	void Start () {
		theAM = FindObjectOfType<Audio_Manager> ();
	}

	void OnTriggerEnter (Collider other){
		if (other.tag == "Player") {
			if(newTrack != null)
			theAM.ChangeMusic (newTrack);
		}
	}
}
