using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Animation_Controller : MonoBehaviour {

    Animator anim;
    AI_Movement ai;
    AI_Controller controller;

    bool justChangedState = false; // Used to trigger audio? If AI 'sees' player, play scream audio?

    [HideInInspector]
    public animationState prevState; // Used to see if we just changed to this state
    // [HideInInspector]
    public animationState state;
    public enum animationState {
        BLOCKED_STATE,
        IDLE,
        MOVING,
        ATTACKING,
        DEAD
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        ai = GetComponent<AI_Movement>();
        controller = GetComponent<AI_Controller>();

        state = animationState.IDLE;
        prevState = state;
	}
	
	// Update is called once per frame
	void Update () {
        if (prevState != state) justChangedState = true;
        else justChangedState = false;

        HandleAnimatorChanges();
        AssignAnimatorBooleans();
    }

    private void HandleAnimatorChanges() {
        if (anim != null && ai != null) {
            if (state != animationState.BLOCKED_STATE) {
                if (ai.nav.velocity.magnitude == 0) {
                    prevState = state;

                    if (!ai.movingToPlayer) {
                        // Means we haven't reached the player and are attacking
                        state = animationState.IDLE;
                    }
                } 
                else {
                    prevState = state;
                    state = animationState.MOVING;
                }
                // Attacking state set by movement script - based on dist to player
                // Dead state set by controller script (health <= 0)
            }
        }
    }

    private void AssignAnimatorBooleans() {
        // Sets all other animator booleans to false to set a single one to true
        foreach(AnimatorControllerParameter parameter in anim.parameters) {
            anim.SetBool(parameter.name, false);
        }

        switch(state) {
            case animationState.DEAD:
                anim.SetBool("isDead", true);
                state = animationState.BLOCKED_STATE;
                break;
            case animationState.ATTACKING:
                anim.SetBool("isAttacking", true);
                state = animationState.BLOCKED_STATE;
                break;
            case animationState.MOVING:
                anim.SetBool("isMoving", true);
                break;
            case animationState.IDLE:
                anim.SetBool("isIdle", true);
                break;
            case animationState.BLOCKED_STATE:
                if(currAnimHasFinished()) {
                    prevState = state;
                    state = animationState.IDLE;
                }
                break;
        }
    }

    public bool currAnimHasFinished() {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !anim.IsInTransition(0)) {
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

    public void reset() {
        state = animationState.IDLE;
        prevState = state;
        justChangedState = false;
    }
}
