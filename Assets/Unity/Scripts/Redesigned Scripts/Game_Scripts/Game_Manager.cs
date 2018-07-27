using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

	Camera_Manager cameraManager;
    Player_Inventory pInv;

    // Key Binds - easy to reconfigure in settings later.
    
    // List of keybinds: (confirm)
    // Attack
    // Interact
    // Use health pack?
    // Use key?
    // Toggle weapon equip?
    // Toggle flashlight on/off?

    public static KeyCode attack = KeyCode.Space;
	public static KeyCode interact = KeyCode.E;
    public static KeyCode useHealth = KeyCode.H;
    public static KeyCode lightToggle = KeyCode.O;
    public static KeyCode weaponToggle = KeyCode.U;
    public static KeyCode inventoryToggle = KeyCode.I;

    public ViewState view;
    public enum ViewState {
        MAIN_MENU,
        SETTINGS,
        INVENTORY,
        GAME
    }

    private float timer = 0;
    private float durToFadeInv = 0.5f;
    private bool toggled = false;

	// Use this for initialization
	void Start () {
		if (GetComponent<Camera_Manager> () != null)
			cameraManager = GetComponent<Camera_Manager> ();
		else
			Debug.LogError ("You need to add a camera manager to the game manager");

        pInv = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();
        // Change later to main menu instead of loading into game
        view = ViewState.GAME;
	}
	
	// Update is called once per frame
	void Update () {
        switch (view) {
            case ViewState.GAME:
                if(Input.GetKeyDown(Game_Manager.inventoryToggle)) {
                    setState(ViewState.INVENTORY);
                    timer = Time.realtimeSinceStartup;
                    StartCoroutine(beginFade(durToFadeInv/2));
                    StartCoroutine(beginUnfade(durToFadeInv/2));
                }

                if (timer != 0 && Time.realtimeSinceStartup >= timer + durToFadeInv/2 && !toggled) {
                    pInv.toggleUI();
                    toggled = true;
                }
                if (timer != 0 && Time.realtimeSinceStartup >= timer + durToFadeInv && toggled) {
                    timer = 0;
                    toggled = false;
                    Time.timeScale = 1;
                }
            break;
            case ViewState.INVENTORY:
                if (Input.GetKeyDown(Game_Manager.inventoryToggle)) {
                    setState(ViewState.GAME);
                    timer = Time.realtimeSinceStartup;
                    StartCoroutine(beginFade(durToFadeInv/2));
                    StartCoroutine(beginUnfade(durToFadeInv/2));
                }

                if (timer != 0 && Time.realtimeSinceStartup >= timer + durToFadeInv / 2 && !toggled) {
                    pInv.toggleUI();
                    toggled = true;
                }
                if (timer != 0 && Time.realtimeSinceStartup >= timer + durToFadeInv && toggled) {
                    timer = 0;
                    toggled = false;
                    Time.timeScale = 0;
                }
            break;
        }
    }


    // Getters and Setters
    public void setState(ViewState state){
		this.view = state;
	}
	public ViewState getState(){
		return view;
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
