//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//// Adapted From GameGrind on youtube
//
//public class Inventory : MonoBehaviour {
//
//	public int slotsX, slotsY;
//	public int activeItem;
//
//	public GUISkin skin;
//
//	public List<Item> inventory = new List<Item> ();
//	public List<Item> slots = new List<Item> ();
//
//	private bool showInventory;
//	private bool showTooltip;
//	private string tooltip;
//
//	private Item_Database database;
//	private Player_Health myPlayerHealth;
//
//	void Start () {
//		activeItem = 1;
//
//		for(int i = 0; i < (slotsX * slotsY); i++){
//			slots.Add (new Item ());
//			inventory.Add (new Item ());
//		}
//
//		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<Item_Database> ();
//		AddItem (1);
//
//		myPlayerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Health> ();
//	}
//
//	void OnGUI () {
//
//		tooltip = "";
//
//		GUI.skin = skin;
//
//		DrawInventory ();
//
//		if (showTooltip) {
//			GUI.Box (new Rect (75, 5, 140, 80), tooltip, skin.GetStyle("Tooltip"));
//		}
//
//
//
//
//		/*for (int i = 0; i < 9; i++)
//		{
//
//			if (i < inventory.Count)
//			{
//
//				if(inventory[i].itemStackable)GUI.Label("" + inventory[i].itemQuantity);
//
//			}
//
//			if (Input.GetKeyDown("" + (i+1)))
//			{
//				activeItem = i;
//			}
//		}*/
//
//
//	}
//
//	void DrawInventory (){
//
//		int i = 0;
//
//		Event currentEvent = Event.current;
//
//		for (int y = 0; y < slotsY; y++){
//			
//			for (int x = 0; x < slotsX; x++)
//			 {
//				Rect slotRect = new Rect (x * 40 + 10, y * 40 + 10, 60, 60);
//				GUI.Box (slotRect, "", skin.GetStyle("Slot"));
//				slots [i] = inventory [i];
//				Item item = slots [i];
//
//				if (slots[i].itemName != null){ // As long as current slot contains item do this
//					GUI.DrawTexture (slotRect, slots[i].itemIcon);
//
//					// Create tooltip
//					if (slotRect.Contains(currentEvent.mousePosition)) {
//						CreateTooltip (slots [i]);
//						showTooltip = true;
//					}
//
//					// Use Item
//					if (currentEvent.isKey && currentEvent.type == EventType.keyDown && currentEvent.keyCode == KeyCode.R){
//						if (item.itemType== Item.ItemType.Consumable){
//							UseConsumable (slots[i], i, true);
//							print ("use consumable");
//						}
//					}
//				}
//
//				if (tooltip == "") { // if tooltip string is blank, don't show the tool tip
//					showTooltip = false;
//				}
//				i++;
//			}
//		}
//	}
//
//	string CreateTooltip(Item item){
//		tooltip = "<color=#00ff00>" + item.itemName + "</color>" + "\n\n" + item.itemDesc;
//		return tooltip;
//	}
//
//	void AddItem(int id){
//
//		for (int i = 0; i < inventory.Count; i++){
//			if (inventory[i].itemName == null){
//				for (int j = 0; j < database.items.Count; j++){
//					if(database.items[j].itemID == id){
//						inventory [i] = database.items [j];
//					}
//				}
//				break;
//			}
//		}
//	}
//
//	/*void RemoveItem(int id){
//		for (int i = 0; i < inventory.Count; i++) {
//			if (inventory [i].itemID == id) {
//				inventory [i] = new Item ();
//				break;
//			}
//		}
//	}*/
//
//	private void UseConsumable(Item item, int slot, bool deleteItem){
//		switch(item.itemID){
//		case 1: // Restores 10 health to the player when used
//			{
//				myPlayerHealth.curHealth = myPlayerHealth.curHealth + 10;
//				break;
//			}
//		}
//
//		if (deleteItem) {
//			inventory[slot] = new Item ();
//		}
//	}
//}
