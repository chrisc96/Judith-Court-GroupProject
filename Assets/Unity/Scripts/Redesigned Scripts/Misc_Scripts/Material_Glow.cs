using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script adapted from dangerdex on unity forums: http://answers.unity3d.com/questions/914923/standard-shader-emission-control-via-script.html

public class Material_Glow : MonoBehaviour {

	void Update () {
		Renderer renderer = GetComponent<Renderer> ();
		Material mat = renderer.material;

		float emission = Mathf.PingPong (Time.time, 1.0f);
		Color baseColor = Color.white; 

		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);

		mat.SetColor ("_EmissionColor", finalColor);
	}
}
