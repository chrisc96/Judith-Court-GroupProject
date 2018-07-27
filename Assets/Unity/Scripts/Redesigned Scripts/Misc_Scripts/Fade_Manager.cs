using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Manager : MonoBehaviour {

    public static Fade_Manager Instance {set; get;}

    public Image fadeImage;
    private bool isInTransition;
    private float transition;
    private bool isShowing;
    private float duration;

    // Use this for initialization
    void Start () {
        Instance = this;
	}

    public void fadeToClear(float duration) {
        isShowing = false;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
    }

    public void unfade(float duration) {
        isShowing = true;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
    }

    private void Update() {
        if (!isInTransition) return;

        transition += (isShowing) ? Time.unscaledDeltaTime * (1/duration) : -Time.unscaledDeltaTime * (1/duration);
        fadeImage.color = Color.Lerp(new Color(0,0,0,0), Color.black, transition);

        if (transition >= 1 || transition <= 0) {
            isInTransition = false;
        }
    }
}
