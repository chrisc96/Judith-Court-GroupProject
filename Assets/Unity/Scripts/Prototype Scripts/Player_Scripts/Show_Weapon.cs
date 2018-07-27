using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Weapon : MonoBehaviour {

	public GameObject weaponEquip;
	public GameObject weaponCollect;
	public bool showWeapon;
	public bool showWeaponCollect;

	private Item_Range inRange;

	// The weapon is always in the players hand, it just need to be activated by 'picking up' the weapon on the ground.
	void Start () {
		showWeapon = false;
		showWeaponCollect = true;

		inRange = GameObject.Find("Weapon Collectable").GetComponent<Item_Range> ();
	}

	void Update () {
		if (showWeapon == false) {
			weaponEquip.SetActive (false);
			weaponCollect.SetActive (true);
		}
		if (showWeapon == true) {
			weaponEquip.SetActive (true);
			weaponCollect.SetActive (false);
		}
		if (Input.GetKeyDown (KeyCode.Alpha1) && showWeapon == false && inRange.withinRange) {
			showWeapon = true;
			showWeaponCollect = false;
		}
	}
}