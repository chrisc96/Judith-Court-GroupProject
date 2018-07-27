using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script adapted from gamesplusjames on Youtube: https://www.youtube.com/watch?v=XoH8Qyqje1g

public class Audio_Manager : MonoBehaviour {

	public AudioSource musicSource;
	 
	public void ChangeMusic(AudioClip music){

		if (musicSource.clip.name == music.name)
			return;
		
		musicSource.Stop ();
		musicSource.clip = music;
		musicSource.Play ();
	}
}
