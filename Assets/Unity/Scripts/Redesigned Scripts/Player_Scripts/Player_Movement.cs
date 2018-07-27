using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    Player_Controller controller;
    Player_Animation_Controller anim;
    Game_Manager gm;

    Rigidbody rb;

    public Vector3 velocity = Vector3.zero;
    Quaternion targetRotation;

    public string FORWARD_AXIS = "Vertical";
    public string TURN_AXIS = "Horizontal";

    public float forwardVel;
    public float boostMultiplier;
    public float rotateVel;
    public float inputDelay;

    float forwardInput, turnInput;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<Player_Controller>();
        anim = GetComponent<Player_Animation_Controller>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();

        targetRotation = transform.rotation;
        forwardInput = turnInput = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(gm.view != Game_Manager.ViewState.INVENTORY) {
            GetInput();
        }
    }

    private void GetInput() {
        forwardInput = Input.GetAxis(FORWARD_AXIS);
        turnInput = Input.GetAxis(TURN_AXIS);
        HandleMovementChanges();
    }

    private void FixedUpdate() {
        // If we're alive and we're not blocked (so an event has stopped us from being able to move/rotate)
        if (anim.state != Player_Animation_Controller.animationState.BLOCKED_STATE && 
            controller.state == Player_Controller.PLAYER_STATE.ALIVE) {
            Turn();
            Run();
            rb.velocity = transform.TransformDirection(velocity);
        }
    }

    void Run() {
        if (Mathf.Abs(forwardInput) >= inputDelay) {
            velocity.z = forwardVel * forwardInput;
        }
        else {
            velocity.z = 0;
        }
    }

    void Turn() {
        if(Mathf.Abs(turnInput) > inputDelay) {
            targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.smoothDeltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }

    void HandleMovementChanges() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            // Multiplies the velocity of the player at a given instant
            forwardVel *= boostMultiplier;
        } 
        else if(Input.GetKeyUp(KeyCode.LeftShift)) {
            forwardVel *= (1/boostMultiplier);
        }
    }

    private void stopMoving() {
        rb.velocity = Vector3.zero;
        forwardInput = turnInput = 0;
    }
}
