using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour {

    [HideInInspector]
    public NavMeshAgent nav;
    public Transform spawnPoint;
    private GameObject playerTarget;
    AI_Animation_Controller anim;

    public float backoffDist; // Distance away the player needs to be to stop chasing - gets set to sphere collider diameter

//     [HideInInspector]
    public bool movingToPlayer = false;
    private bool respawned = true;

    // Use this for initialization
    void Start () {
		if (GetComponent<NavMeshAgent>() != null) nav = GetComponent<NavMeshAgent>();
        else { Debug.LogError("You need to add a nav mesh agent"); }
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        if (GetComponent<SphereCollider>() != null) GetComponent<SphereCollider>().radius = backoffDist/2;
        else { Debug.LogError("You need to add sphere collider"); }
        if (spawnPoint == null) Debug.LogError("You need to add a spawnpoint");
        anim = GetComponent<AI_Animation_Controller>();

        transform.position = determineRespawnPos(spawnPoint.position, 15);
    }

    void Update () {
        HandleMovement();
        HandlePlayerDeathReset();
    }

    private void HandlePlayerDeathReset() {
        if (playerTarget.GetComponent<Player_Controller>().state == Player_Controller.PLAYER_STATE.DEAD) {
            if (respawned) {
                respawned = false;
            }
        }
        else {
            if (!respawned) {
                respawned = true;
                reset();
                anim.reset();
            }
        }
    }

    private void HandleMovement() {
        if(nav.enabled) {
            // block movement if in blocked state or dead
            if(anim.state != AI_Animation_Controller.animationState.BLOCKED_STATE &&
                anim.state != AI_Animation_Controller.animationState.DEAD) {
                // Chase player
                if(movingToPlayer) {
                    nav.SetDestination(playerTarget.transform.position);
                    lookAtPosition(playerTarget.transform.position);
                    float dist = Vector3.Distance(transform.position, playerTarget.transform.position);
                    Vector3 dir = playerTarget.transform.position - transform.position;
                    float angle = Vector3.Dot(dir, transform.up);
                    if(dist < nav.stoppingDistance) {
                        // Attack player
                        anim.prevState = anim.state;
                        anim.state = AI_Animation_Controller.animationState.ATTACKING;
                    }
                } 
                else {
                    nav.isStopped = true;
                    nav.SetDestination(determineRespawnPos(spawnPoint.position, 15));
                    lookAtPosition(spawnPoint.position);
                    nav.isStopped = false;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            movingToPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            movingToPlayer = false;
        }
    }

    public Vector3 determineRespawnPos(Vector3 currPos, int length) {
        Vector3 rayPos = new Vector3(currPos.x, currPos.y + length / 2, currPos.z);
        RaycastHit hit;
        if(Physics.Raycast(rayPos, -Vector3.up, out hit, length)) {
            return new Vector3(currPos.x, hit.point.y, currPos.z);
        }
        else {
            return currPos;
        }
    }


    public void reset() {
        movingToPlayer = false;
        transform.position = determineRespawnPos(spawnPoint.position, 15);
    }

    private void lookAtPosition(Vector3 pos) {
        Vector3 vectorToPos = transform.InverseTransformPoint(pos);
        float rotationAngle = Mathf.Atan2(vectorToPos.x, vectorToPos.z) * Mathf.Rad2Deg;
        Vector3 rotationVelocity = (Vector3.up * rotationAngle) * nav.angularSpeed * Time.deltaTime;
        Vector3 deltavel = (rotationVelocity - GetComponent<Rigidbody>().angularVelocity);
        GetComponent<Rigidbody>().AddTorque(deltavel, ForceMode.Acceleration);
    }
}