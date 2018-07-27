using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight_Listener : MonoBehaviour {

    private Button keyButton;

    private Image img;
    public Sprite activeImage;
    public Sprite inactiveImage;

    Flashlight fl;

    // Use this for initialization
    void Start() {
        img = GetComponent<Image>();
        keyButton = GetComponent<Button>();
        keyButton.onClick.AddListener(TaskOnClick);
        fl = GameObject.FindGameObjectWithTag("Player").GetComponent<Flashlight>();
    }

    void TaskOnClick() {
        fl.isOn = !fl.isOn;
    }

    private void Update() {
        if(fl.isOn) {
            img.sprite = activeImage;
        } else {
            img.sprite = inactiveImage;
        }
    }
}