//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class PickupItem : MonoBehaviour {
//
//	private Item_Database db;
//	private Inventory invent;
//	private Item_range inRange;
//
//	// Use this for initialization
//	void Start () {
//		db = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<Item_Database>();
//		invent = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory>();
//		inRange = GameObject.FindGameObjectWithTag ("Object").GetComponent<Item_range>();
//	}
//		
//	void Update () {
//		if (inRange.withinRange && Input.GetKeyDown(KeyCode.Alpha1)) {
//			if (db.items[item].itemStackable && invent.inventory.Contains(db.items[item])) { // If stackable and inventory contains the item already:
//				invent.inventory[invent.inventory.IndexOf(db.items[item])].itemQuantity++;
//				Destroy(gameObject);
//			}
//
//			else if (db.items[item].itemStackable && invent.inventory.Count < 9)
//			{
//				invent.inventory.Add(db.items[item]);
//				Destroy(gameObject);
//
//			}
//
//			else if(invent.inventory.Count<9)
//			{
//				invent.inventory.Add(db.items[item]);
//				Destroy(gameObject);
//			}
//			/*GameObject HP = GameObject.FindGameObjectWithTag ("Object");
//			//if (gameObject.CompareTag("Object")){
//			HP.gameObject.SetActive (false);
//			Debug.Log (HP);
//			//}*/
//		}
//	}
//}