using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour {

    public GameObject inventoryUI;
    Boolean showing = false;
    public List<Item> items = new List<Item>();

    private void Start() {
    }

    public void toggleUI() {
        showing = !showing;
        if (showing) inventoryUI.SetActive(true);
        else {
            inventoryUI.SetActive(false);
        }
    }

    // Accessor methods for items
    public void addToInventory(Item item) {
        items.Add(item);
    }

    public bool hasItemType(Item.ItemType type) {
        foreach(Item itm in items) {
            if(itm.itemType == type) return true;
        }
        return false;
    }

    public Item getItemByType(Item.ItemType type) {
        foreach(Item itm in items) {
            if(itm.itemType == type) return itm;
        }
        return null;
    }

    public List<Item> getItemsByType(Item.ItemType type) {
        List<Item> itms = new List<Item>();
        foreach(Item itm in items) {
            if (itm.itemType == type) {
                itms.Add(itm);
            }
        }
        return itms;
    }


    // Weapons


    public bool isWeaponVisible() {
        GameObject hand = GameObject.FindGameObjectWithTag("Hand");
        foreach(Transform child in hand.transform) {
            if (child.gameObject.activeSelf == false) return false;
            else return true;
        }
        return false;
    }

    public void showHideWeapon() {
        if (!isWeaponVisible()) {
            showWeapon();
        }
        else if (isWeaponVisible()) {
            hideWeapon();
        }
    }

    public void showWeapon() {
        Item itm = getItemByType(Item.ItemType.BaseballBat);
        // Convoluted, messy but it'll do the trick
        if (itm == null) return;
        GameObject equippedSword = itm.gameObject.GetComponent<Item_Pickup>().equipItem;
        equippedSword.SetActive(true);
    }

    public void hideWeapon() {
        Item itm = getItemByType(Item.ItemType.BaseballBat);
        // Convoluted, messy but it'll do the trick
        if (itm == null) return;
        GameObject equippedSword = itm.gameObject.GetComponent<Item_Pickup>().equipItem;
        equippedSword.SetActive(false);
    }

    // Health Packs
    public int getNumHealthPacks() {
        // Should only ever be one (with stacks) or none
        Item itm = getItemByType(Item.ItemType.HealthPack);
        if (itm == null) return 0;
        else {
            return itm.itemQuantity;
        }
    }

    public void removeHPAfterUse() {
        Item itm = getItemByType(Item.ItemType.HealthPack);
        if (itm == null) return;
        // Only one stack left, remove health pack from list
        if (itm.itemQuantity == 1) {
            items.Remove(itm);
        }
        else {
            itm.itemQuantity--;
        }
    }

    public bool tryUseHP() {
        Player_Controller pc = GetComponent<Player_Controller>();
        if (getNumHealthPacks() == 0) return false;
        else if (pc.health == pc.maxHealth && getNumHealthPacks() == 1) return false;
        else {
            pc.adjustHealth(getItemByType(Item.ItemType.HealthPack).HP_healthIncrease);
            removeHPAfterUse();
            return true;
        }
    }

    // Keys
    public List<Item> getKeys() {
        return getItemsByType(Item.ItemType.Key);
    }

    // Probably used by the doors to check if player has matching key
    public bool hasKeyWithID(int id) {
        List<Item> keys = getKeys();
        if (keys == null) return false;
        foreach (Item itm in keys) {
            if (itm.KeyID == id) {
                return true;
            }
        }
        return false;
    }

    public Item getKeyWithID(int id) {
        List<Item> keys = getKeys();
        if(keys == null) return null;
        foreach(Item itm in keys) {
            if (itm.KeyID == id) {
                return itm;
            }
        }
        return null;
    }

    public void removeKeyWithID(int id) {
        List<Item> keys = getKeys();
        if(keys == null) return;
        foreach(Item itm in keys) {
            if(itm.KeyID == id) {
                items.Remove(itm);
                break; // Only have one key with one ID
            }
        }
    }


    // Helpers
    public static int getNumGameobjectChildren(Transform trans) {
        int count = 0;
        foreach(Transform child in trans) {
            count++;
        }
        return count;
    }
}