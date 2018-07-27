using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_Script : MonoBehaviour {

    public Image content;
    private Player_Controller pc;

    // Use this for initialization
    void Start() {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void Update() {
        content.fillAmount = (pc.health / pc.maxHealth);
    }
}
