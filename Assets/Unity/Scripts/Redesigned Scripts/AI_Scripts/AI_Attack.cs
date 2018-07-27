using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Attack : MonoBehaviour {

    List<AI_Hit_Target> hands = new List<AI_Hit_Target>();
    Rigidbody rb;
    Player_Controller p_control;
    AI_Animation_Controller anim;

    bool usedAttack = false;
    bool knockBack = false;

    public int damage;

	// Use this for initialization
	void Start () {
        hands.AddRange(GetComponentsInChildren<AI_Hit_Target>());
        p_control = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        anim = GetComponent<AI_Animation_Controller>();
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		foreach (AI_Hit_Target hit in hands) {
            if (hit.hitPlayer && !usedAttack) {
                attackPlayer();
                usedAttack = true;
            }
        }

        if (anim.currAnimHasFinished() && anim.prevState == AI_Animation_Controller.animationState.ATTACKING) {
            usedAttack = false;
        }
	}

    private void attackPlayer() {
        p_control.adjustHealth(damage);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.CompareTag("Weapon")) {
            if(p_control.animationHandler.state == Player_Animation_Controller.animationState.BLOCKED_STATE) {
                HandleKnockback(collision);
                HurtEnemy();
            }
        }
    }

    private void HurtEnemy() {
        Item itm = p_control.inventoryHandler.getItemByType(Item.ItemType.BaseballBat);
        if(itm == null) return;
        else {
            gameObject.GetComponent<AI_Controller>().adjustEnemyHealth(itm.damage);
        }
    }

    private void HandleKnockback(Collision collision) {        
        // If a weapon hit us, we should be knocked back.        
        Vector3 contactPoint = new Vector3(1000000, 1000000, 1000000);
        foreach(ContactPoint cp in collision.contacts) {
            if(Vector3.Distance(cp.point, collision.collider.transform.position) < contactPoint.magnitude) {
                contactPoint = cp.point;
            }
        }
        Vector3 dir = -(collision.collider.gameObject.transform.position - contactPoint).normalized;
        if(dir.y <= 0) dir.y = 0;
        contactPoint = new Vector3(contactPoint.x, contactPoint.y + (contactPoint.y - GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents.y), contactPoint.z);
        rb.AddForceAtPosition(dir * 7.5f, contactPoint, ForceMode.Impulse);
    }
}
