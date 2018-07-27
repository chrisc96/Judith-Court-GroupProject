using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class PlayVideo : MonoBehaviour {

 public MovieTexture movie;
 private AudioSource audio_movie;

 // Use this for initialization
 void Start () {
	GetComponent<RawImage>().texture = movie as MovieTexture;
	audio_movie = GetComponent<AudioSource>();
	audio_movie.clip = movie.audioClip;
	movie.Play ();
	audio_movie.Play ();
 }

 void Update () {
	  if (Input.GetKeyDown (KeyCode.Space) && movie.isPlaying) {
	   	movie.Pause ();  
	  }
	  else if (Input.GetKeyDown (KeyCode.Space) && !movie.isPlaying){
	  	movie.Play();  
	  }

	}
     
}﻿