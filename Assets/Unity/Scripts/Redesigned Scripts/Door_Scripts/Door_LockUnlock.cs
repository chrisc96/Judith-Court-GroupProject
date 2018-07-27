using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_LockUnlock : MonoBehaviour {

	 [HideInInspector]
    public bool open = false;
    Player_Inventory p_inv;

    [Header("Only need this script if it's an unlockable door. Normal doors don't need this")]
    public int DoorID = -1;

    private void Start() {
        p_inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();

        if (tag != "LockUnlockDoor") Debug.LogError("You need to add a LockUnlockDoor tag to this object");
        if (DoorID == -1) Debug.LogError("You forgot to assign a door ID to match with a key ID");
    }
		
    public void setDoorOpen(bool b) {
        open = b;
    }

    public bool playerHasMatchingKey() {
        return p_inv.hasKeyWithID(DoorID);
    }


}