using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pickup : MonoBehaviour {

    // Needs to be added to player inventory
    Player_Inventory p_inv;
    Item item;
    Item_Range rangeCheck;

    [Header("If item type is weapon, drag model to put on player here")]
    public GameObject equipItem;

	void Start () {
        if (tag != "Item")
            Debug.LogError("You need to add the Item tag to this object. You've added a pickup item script");

        if ((item = GetComponent<Item>()) == null)
            Debug.Log("You need to add an Item script");

        if ((rangeCheck = GetComponent<Item_Range>()) == null)
            Debug.Log("You need to add a Item Range script");

        if (GetComponent<Item_Pickup>() == null)
            Debug.LogError("You need to add an item pickup script");

        p_inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();
    }
	
	void Update () {
        // Only want to pickup an item if it's active in hierarchy
		if (rangeCheck.withinRange && Input.GetKeyDown(Game_Manager.interact) && gameObject.activeInHierarchy) {
            switch (item.itemType) {
                case Item.ItemType.BaseballBat:
                    pickupWeapon();
                    break;
                case Item.ItemType.HealthPack:
                    pickupHealthpack();
                    break;
                case Item.ItemType.Key:
                    pickupKey();
                    break;
            }

            if(GetComponent<Show_Note>() != null) {
                GetComponent<Show_Note>().note.gameObject.SetActive(false);
            }
        }
	}

    private void pickupWeapon() {
        // Shouldn't ever already have a weapon in the inventory
        if (!p_inv.hasItemType(item.itemType)) {
            p_inv.addToInventory(item);
            GameObject playersHand = GameObject.FindGameObjectWithTag("Hand");
            if (equipItem != null) {
                equipItem.SetActive(true);
            }
            else {
                Debug.LogError("Did you forget to attach a gameobject to your collectible gameobject?");
            }
        }
        gameObject.SetActive(false); // Picked up, so disappear
    }

    private void pickupHealthpack() {
        if (!p_inv.hasItemType(item.itemType)) {
            p_inv.addToInventory(item);
            item.itemQuantity++;
        }
        else {
            p_inv.getItemByType(item.itemType).itemQuantity++;
        }
        gameObject.SetActive(false); // Picked up, so disappear
    }

    private void pickupKey() {
        p_inv.addToInventory(item);  // We can have multiple keys (non stackable)
        gameObject.SetActive(false); // Picked up, so disappear
    }
}