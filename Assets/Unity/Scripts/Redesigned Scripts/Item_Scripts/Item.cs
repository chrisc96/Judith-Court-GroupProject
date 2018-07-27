using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Adapted from GameGrind on youtube

[System.Serializable]
public class Item : MonoBehaviour {

    // Accessed via player inventory manager
    [HideInInspector]
    public string itemName;
    [HideInInspector]
    public string itemDesc;
    [HideInInspector]
    public int itemQuantity = 0;

    public ItemType itemType;
	public enum ItemType {
		BaseballBat,
		HealthPack,
		Key
	}

    [Header("Only set if type is key")]
    public int KeyID = -1;  // -1 for non key items
                            // Should match to a door code

    [HideInInspector]
    public int damage = 0;

    [HideInInspector]
    public int HP_healthIncrease = 0;
    private void Start() {
        if (itemType == ItemType.Key && KeyID == -1) {
            Debug.LogError("You forgot to set a key ID");
        }
        if (itemType == ItemType.HealthPack) {
            HP_healthIncrease = 20;
        }
        if (itemType == ItemType.BaseballBat) {
            damage = -20;
        }
    }
}