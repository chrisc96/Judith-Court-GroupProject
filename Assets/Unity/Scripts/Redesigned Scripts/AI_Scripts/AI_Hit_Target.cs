using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Hit_Target : MonoBehaviour {

    AI_Animation_Controller anim;
    public bool hitPlayer;

	// Use this for initialization
	void Start () {
		anim = GetComponentInParent<AI_Animation_Controller>();
	}
	
	// Update is called once per frame
	void Update () {
        // If we're currently in the attacking motion
		if (anim.prevState == AI_Animation_Controller.animationState.ATTACKING) {
                if (anim.currAnimHasFinished()) {
                // Shouldn't contact the player here
                    hitPlayer = false;
                }
        }
    }

    private void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Player") && !col.isTrigger) {
            if (!hitPlayer) {
                hitPlayer = true;
            }
        }
    }
}
