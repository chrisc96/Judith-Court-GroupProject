using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : MonoBehaviour {

    AI_Animation_Controller animController;
    AI_Movement movement;

    private GameObject player;

    [HideInInspector]
    public int maxHealth = 100;
    [HideInInspector]
    public int currHealth = 100;

    [HideInInspector]
    public aiState state;
    public enum aiState {
        ALIVE,
        DEAD,
    }

    public float respawnTimeInSecs;    // How long do you want to wait before this AI respawns
    private float respawnTimer = -1;  // != 0 means it's dead and we're counting down to respawn
                                     // == 0 means we can respawn

    private bool ableToSpawn = false; // True if respawnTimer == 0 (except by default as AI already loaded)

    // Use this for initialization
    void Start () {
        if (GetComponent<AI_Animation_Controller>() != null) animController = GetComponent<AI_Animation_Controller>();
        else Debug.LogError("You need to add an animation controller to this AI");
        if(GetComponent<AI_Movement>() != null) movement = GetComponent<AI_Movement>();
        else Debug.LogError("You need to add an AI_Movement script to this AI");

        player = GameObject.FindGameObjectWithTag("Player");
        state = aiState.ALIVE;
    }
	
	// Update is called once per frame
	void Update () {
        switch (state) {
            case aiState.ALIVE:
                break;
            case aiState.DEAD:
                // If death animation has finished, then prep for respawn
                if (animController.prevState == AI_Animation_Controller.animationState.DEAD) {
                    fullReset();
                }
                break;
        }
        HandleRespawn();
    }

    // #Gross - sorry
    private void HandleRespawn() {
        // Means the AI must've died, count down to respawn
        if (respawnTimer != 0 && respawnTimer != -1) {
            respawnTimer -=  Time.unscaledDeltaTime;
        }

        if (respawnTimer <= 0 && respawnTimer != -1) {
            ableToSpawn = true;
            respawnTimer = -1;
        }

        if (ableToSpawn) {
            Vector3 vectorToPlayer = player.transform.position - transform.position;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, vectorToPlayer, out hit)) {
                if (hit.collider.tag == "Player") {
                    // If we can see the player, don't respawn
                    return;
                } 
                else {
                    // AI should now be respawned at the spawn point
                    GetComponentInChildren<Renderer>().enabled = true;
                    foreach(Collider col in GetComponents<Collider>()) {
                        col.enabled = false;
                    }
                    GetComponent<NavMeshAgent>().enabled = true;
                    GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
    }

    public void adjustEnemyHealth(int adj) {
        currHealth += adj;

        if (currHealth <= 0) {
            currHealth = 0;
            state = aiState.DEAD;
            animController.state = AI_Animation_Controller.animationState.DEAD;
        }
        if (currHealth >= maxHealth) {
            currHealth = maxHealth;
        }

        Debug.Log(currHealth);
    }

    // If AI dies, reset and set inactive until time to respawn
    public void fullReset() {
        Debug.Log("HERE");
        animController.reset();
        movement.reset();
        state = aiState.ALIVE;
        ableToSpawn = false;
        respawnTimer = respawnTimeInSecs;
        currHealth = maxHealth;
        GetComponentInChildren<Renderer>().enabled = false;
        foreach (Collider col in GetComponents<Collider>()) {
            col.enabled = false;
        }
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log("HERE2");
    }
}