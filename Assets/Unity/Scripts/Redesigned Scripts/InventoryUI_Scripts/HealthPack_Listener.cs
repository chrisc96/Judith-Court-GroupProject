using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPack_Listener : MonoBehaviour {

    private Button keyButton;
    public Text numHPs;
    private Player_Inventory pInv;

    private Image img;
    public Sprite activeImage;
    public Sprite inactiveImage;

    // Use this for initialization
    void Start() {
        pInv = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();
        img = GetComponent<Image>();
        keyButton = GetComponent<Button>();
        keyButton.onClick.AddListener(TaskOnClick);
        numHPs.text = "" + pInv.getNumHealthPacks();
    }

    void TaskOnClick() {
        pInv.tryUseHP();
    }

    private void Update() {
        int num = pInv.getNumHealthPacks();
        numHPs.text = "" + pInv.getNumHealthPacks();
        if (num > 0) {
            img.sprite = activeImage;
        }
        else {
            img.sprite = inactiveImage;
        }
    }
}