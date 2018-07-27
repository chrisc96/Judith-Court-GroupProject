using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    // All other scripts that are added to the player should be added here. Acts like a manager
    [HideInInspector]
    public Game_Manager manager;
    [HideInInspector]
    public Player_Movement movementHandler;
    [HideInInspector]
    public Player_Animation_Controller animationHandler;
    [HideInInspector]
    public Player_Inventory inventoryHandler;

    [HideInInspector]
	public float height;

	// Set to an initial empty gameObject to spawn at.
	// Then in code will be changed when moving through
	// certain doors (used as respawn point too)
	public Transform spawnPos;
	// When we respawn, we need a reference of which camera
	// we want to look at the player and set active
	[HideInInspector]
	public GameObject cameraRespawnLookingAtPlayer;

    //[HideInInspector]
    public float health = 100;
    [HideInInspector]
    public float maxHealth = 100;

	private float fadeTimer = 0;
	private float timeToFade = 1.5f;

    public PLAYER_STATE state;
    public enum PLAYER_STATE {
        ALIVE,
        DEAD,
    }

    [HideInInspector]
	public bool isTeleporting = false; // Currently teleporting (to block movement etc)


	// Use this for initialization
	void Start () {
        if((movementHandler = GetComponent<Player_Movement>()) == null)
            Debug.LogError("You need to add a player movement script to this game object");

        if((animationHandler = GetComponent<Player_Animation_Controller>()) == null)
            Debug.LogError("You need to add a player animation handler script to this game object");

        if((manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>()) == null)
            Debug.LogError("You're missing a Game Manager with a game manager script in your scene");

        if((inventoryHandler = GetComponent<Player_Inventory>()) == null)
            Debug.LogError("You need to add a player inventory script to this game object");

        if((GameObject.FindGameObjectWithTag("Hand") == null))
            Debug.LogError("You need to add a hand tag to a part of the player body (weapon pickup)");

        height = GetComponent<MeshRenderer> ().bounds.size.y;
        state = PLAYER_STATE.ALIVE;

		transform.position = determineRespawnPos(spawnPos.position, 15);
    }

    private void Update() {
		switch (state) {
            case PLAYER_STATE.ALIVE:
                break;
            case PLAYER_STATE.DEAD:
				resetPlayer();
				break;
		}
        HandleRespawn();
    }


    // Helper / Util methods
    private void HandleRespawn() {
        // We've been teleported to the new position on respawn
        if(Time.realtimeSinceStartup >= fadeTimer + timeToFade / 2 && fadeTimer != 0) {
            transform.position = determineRespawnPos(spawnPos.position, 15); // Set player pos to current respawn point
            Camera_Manager cm = manager.GetComponent<Camera_Manager>();
            cm.DeactivateAllCameras();
            fadeTimer = -1;
            cameraRespawnLookingAtPlayer.SetActive(true);
        }
        if(Time.realtimeSinceStartup >= fadeTimer + timeToFade && fadeTimer == -1) {
            fadeTimer = 0;
            Time.timeScale = 1;
            state = PLAYER_STATE.ALIVE; // Finally set us back to alive
        }
    }

    public void resetPlayer() {
        // Have we already executed?
        if (fadeTimer == 0) {
            Time.timeScale = 0;
            animationHandler.state = Player_Animation_Controller.animationState.IDLE;
            health = maxHealth;
            fadeTimer = Time.realtimeSinceStartup;
            StartCoroutine(beginUnfade(timeToFade / 2));
            StartCoroutine(beginFade(timeToFade / 2));
        }
    }

    public void adjustHealth(int adjust) {
        health += adjust;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health <= 0) {
            state = PLAYER_STATE.DEAD;
        }
    }

    public Vector3 determineRespawnPos(Vector3 currPos, int length) {
		Vector3 rayPos = new Vector3 (currPos.x, currPos.y + length/2, currPos.z);
		RaycastHit hit;
		if (Physics.Raycast (rayPos, -Vector3.up, out hit, length)) {
			return new Vector3 (currPos.x, hit.point.y, currPos.z);
		}
		else {
			return currPos;
		}
	}

    // Fade/Unfade used when respawning
    private IEnumerator beginUnfade(float unFadeTime) {
        Fade_Manager.Instance.unfade(unFadeTime);
        yield return new WaitForSecondsRealtime(unFadeTime);
    }

    private IEnumerator beginFade(float fadeTime) {
        yield return new WaitForSecondsRealtime(fadeTime);
        Fade_Manager.Instance.fadeToClear(fadeTime);
    }
}