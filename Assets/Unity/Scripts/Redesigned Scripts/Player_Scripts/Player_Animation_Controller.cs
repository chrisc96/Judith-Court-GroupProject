using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation_Controller : MonoBehaviour {

    Animator anim;
    Player_Movement player;
    Player_Inventory playerInventory;

    bool isAttackKeyDown;

    public animationState state;
    public enum animationState {
        BLOCKED_STATE,
        WALKING,
        RUNNING, 
        ATTACKING,
        DEAD,
        IDLE,
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        player = GetComponent<Player_Movement>();
        playerInventory = GetComponent<Player_Inventory>();

        state = animationState.IDLE;
    }

    // Update is called once per frame
    void Update () {
        HandleAnimatorChanges();
        AssignAnimatorBooleans();
    }

    private void HandleAnimatorChanges() {
        if (anim != null && player != null) {
            if (state != animationState.BLOCKED_STATE) {
                if (player.velocity.magnitude == 0) {
                    state = animationState.IDLE;
                }
                else {
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        state = animationState.RUNNING;
                    }
                    else {
                        state = animationState.WALKING;
                    }
                }
                if (Input.GetKeyDown(Game_Manager.attack) && !inAnimatorState("Attack")) {
                    isAttackKeyDown = true;
                }
				if (isAttackKeyDown && playerInventory.isWeaponVisible()) {
                    state = animationState.ATTACKING;
				}

                // TODO: Can add other animation states here, like a pickup animation etc.
            }
        }
    }

    private void AssignAnimatorBooleans() {
        // Sets all other animator booleans to false to set a single one to true
        foreach (AnimatorControllerParameter parameter in anim.parameters) {
            anim.SetBool(parameter.name, false);
        }

        switch(state) {
            case animationState.ATTACKING:
                anim.SetBool("isAttacking", true);
                GameObject.FindGameObjectWithTag("Weapon").GetComponent<BoxCollider>().isTrigger = false;
                // Blocked state represents a state where the current action has to finish before it becomes unblocked
                state = animationState.BLOCKED_STATE;
                break;
            case animationState.DEAD:
                anim.SetBool("isDead", true);
                state = animationState.BLOCKED_STATE;
                break;
            case animationState.RUNNING:
                anim.SetBool("isRunning", true);
                break;
            case animationState.WALKING:
                anim.SetBool("isWalking", true);
                break;
            case animationState.IDLE:
                anim.SetBool("isIdle", true);
                break;
            case animationState.BLOCKED_STATE:
                if (currAnimHasFinished()) {
                    // Input buffering after animation finishes
                    if (Input.GetAxis("Vertical") != 0) {
                        if (Input.GetKey(KeyCode.LeftShift)) {
                            state = animationState.RUNNING;
                        }
                        else {
                            state = animationState.WALKING;
                        }
                    }
                    else {
                        state = animationState.IDLE;
                    }
                    isAttackKeyDown = false;
                    GameObject.FindGameObjectWithTag("Weapon").GetComponent<BoxCollider>().isTrigger = true;
                }
                break;
        }
    }

    // Used to check if animation has finished to swap back from blocked state to idle
    private bool currAnimHasFinished() {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0)) {
            return true;
        }
        return false;
    }

    // Checks if animator is currently in 'animName' state
    private bool inAnimatorState(String animName) {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName(animName)) {
            return true;
        }
        return false;
    }
}