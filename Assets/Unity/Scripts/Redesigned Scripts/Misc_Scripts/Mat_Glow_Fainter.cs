using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat_Glow_Fainter : MonoBehaviour {

	void Update () {
		Renderer renderer = GetComponent<Renderer> ();
		Material mat = renderer.material;

		float emission = Mathf.PingPong (Time.time, 1.0f);
		Color baseColor = Color.grey; 

		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);

		mat.SetColor ("_EmissionColor", finalColor);
	}
}
