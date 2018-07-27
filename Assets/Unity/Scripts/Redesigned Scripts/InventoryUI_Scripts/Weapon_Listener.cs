using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Listener : MonoBehaviour {

    Player_Inventory pi;
    private Button weaponButton;

    public Sprite activeImage;
    public Sprite inactiveImage;
    private Image img;

    // Use this for initialization
    void Start() {
        img = GetComponent<Image>();
        weaponButton = GetComponent<Button>();
        weaponButton.onClick.AddListener(TaskOnClick);
        pi = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();
    }

    void TaskOnClick() {
        pi.showHideWeapon();
    }

    private void Update() {
        if (pi.isWeaponVisible()) {
            img.sprite = activeImage;
        }
        else if (!pi.isWeaponVisible()) {
            img.sprite = inactiveImage;
        }
    }
}