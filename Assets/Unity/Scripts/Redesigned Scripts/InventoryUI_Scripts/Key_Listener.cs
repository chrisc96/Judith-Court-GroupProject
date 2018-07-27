using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key_Listener : MonoBehaviour {

    Player_Inventory pi;

    private Button keyButton;

    private Image img;
    public Sprite activeImage;
    public Sprite inactiveImage;

    // Use this for initialization
    void Start () {
        img = GetComponent<Image>();
        keyButton = GetComponent<Button>();
        keyButton.onClick.AddListener(TaskOnClick);
        pi = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();
    }

    void TaskOnClick() {
        // Tries to teleport through the door
        if (getDoorToTeleportThrough() != null) {
            getDoorToTeleportThrough().GetComponent<Door_Teleport_Trigger>().tryTeleport();
        }
    }

    private void Update() {
        if (pi.getKeys() == null || pi.getKeys().Count == 0) {
            img.sprite = inactiveImage;
        }
        else if (pi.getKeys().Count > 0) {
            img.sprite = activeImage;
        }
    }

    // Goes through the list of doors in the scene, if the player is inside the collider of a normal door,
    // he can teleport. If he's within the collider and he has the key to open the door or the locked door
    // has been opened, we can teleport. Thus we return that door gameobject that the script is attached to
    public GameObject getDoorToTeleportThrough() {
        List<GameObject> doors = new List<GameObject>(GameObject.FindGameObjectsWithTag("NormalDoor"));
        List<GameObject> otherDoors = new List<GameObject>(GameObject.FindGameObjectsWithTag("LockUnlockDoor"));
        doors.AddRange(otherDoors);

        foreach(GameObject go in doors) {
            if (go.GetComponent<Door_Teleport_Trigger>() != null) {
                return go;
            }
            else {
                Debug.LogError("You have a door with a door tag (noraml or teleport) without a door teleport trigger");
            }
        }
        return null;
    }
}