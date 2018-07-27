using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door_Teleport_Trigger : MonoBehaviour {

	//Locked message stuff

	public Transform Door_Locked;
	public bool canView = false;

	GameObject player;
	Player_Controller player_controller;
	Camera_Manager cameraManager;

	public GameObject cameraToMoveTo;
	public Transform placeToMoveTo;
    public float durationToFade;

	private float fadeTimer = 0.0f;
    // [HideInInspector]
    public bool canTeleport = false;

	Vector3 posOfPlace;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		player_controller = player.GetComponent<Player_Controller> ();
        cameraManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Camera_Manager> ();
    }

	void Update() {
		
		if (Input.GetKeyDown (Game_Manager.interact) && canView == true){
			if (!Door_Locked.gameObject.activeInHierarchy){
				Door_Locked.gameObject.SetActive (true);
				Time.timeScale = 0;
			} 
			else {
				Door_Locked.gameObject.SetActive (false);
				Time.timeScale = 1;
			}
		}

        if (Input.GetKeyDown(Game_Manager.interact)) {
            tryTeleport();
        }
        // Used in conjunction with tryTeleport() to determine given a fadeTime
        // if we have teleported yet or not
        CheckTeleported();
	}

    // Called when interact button is pressed or inventory button to use key is pressed
    public void tryTeleport() {
        if(canTeleport && !player_controller.isTeleporting) {
            if (placeToMoveTo != null && cameraToMoveTo != null) {
                posOfPlace = placeToMoveTo.transform.localPosition;
                float distToTrace = 30.0f;
                // Shifts the y pos origin upwards to look down just incase position is below the place of iteraction
                Vector3 rayPos = new Vector3(posOfPlace.x, posOfPlace.y + distToTrace / 2, posOfPlace.z);
                RaycastHit hit;
                if (Physics.Raycast(rayPos, -Vector3.up, out hit, distToTrace)) {
                    player_controller.isTeleporting = true;
                    posOfPlace = new Vector3(posOfPlace.x, hit.point.y + player_controller.height / 2, posOfPlace.z);
                    fadeTimer = Time.time;
                    StartCoroutine(beginUnfade(durationToFade / 2));
                    StartCoroutine(beginFade(durationToFade / 2));
                } 
                else {
                    Debug.LogError("Raycast from teleport pos didn't find a collider below it?");
                }
            } 
            else {
                Debug.LogError("You forgot to add somewhere to move to or a camera to switch to");
            }
        }
    }

    private void CheckTeleported() {
        // We've been teleported to the new position
        if(Time.time >= fadeTimer + durationToFade / 2 && fadeTimer != 0.0f) {
            // Move player to pos
            player.transform.position = posOfPlace;

            // Handle camera changes
            cameraManager.DeactivateAllCameras();
            cameraToMoveTo.SetActive(true);

            // Handle state changes
            player_controller.isTeleporting = false;
            canTeleport = false;

            // Handle timer changes
            fadeTimer = 0.0f;

            // Change door state if not opened
            if(tag == "LockUnlockDoor") {
                Door_LockUnlock door = GetComponent<Door_LockUnlock>();
                // Haven't opened door yet
                if(!door.open) {
                    door.setDoorOpen(true);
                    player_controller.inventoryHandler.removeKeyWithID(door.DoorID);
                }
            }

            // Set respawn values
            player_controller.spawnPos.position = posOfPlace;
            player_controller.cameraRespawnLookingAtPlayer = cameraToMoveTo;
        }
    }

    void OnTriggerStay(Collider obj) {
		if (obj.CompareTag("Player") || obj.CompareTag("Weapon")) {
            // If it's an unlockable door not a normal one
            if (tag == "LockUnlockDoor") {
				canView = true;
                // If the player's inventory has the key or it's open already
                Door_LockUnlock door = GetComponent<Door_LockUnlock>();
                if (door == null) Debug.LogError("You probably forgot to add a door_Lock_Unlock script");
                if (door.playerHasMatchingKey() || door.open) {
					canView = false;
					canTeleport = !canView;
                }
            }
            // Normal door
            else {
				canView = false;
                canTeleport = true;
            }
		}
	}

	void OnTriggerExit(Collider obj) {
		canView = false;
		canTeleport = false;
	}

    private IEnumerator beginUnfade(float unFadeTime) {
        Fade_Manager.Instance.unfade(unFadeTime);
        yield return new WaitForSecondsRealtime(unFadeTime);
    }

    private IEnumerator beginFade(float fadeTime) {
        yield return new WaitForSecondsRealtime(fadeTime);
        Fade_Manager.Instance.fadeToClear(fadeTime);
    }
}