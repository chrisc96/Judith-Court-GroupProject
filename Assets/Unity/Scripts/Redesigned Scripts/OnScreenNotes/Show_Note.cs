using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Note : MonoBehaviour {

    public Transform note;

    void OnTriggerStay(Collider coll) {
        if (coll.CompareTag("Player")) {
            if (!note.gameObject.activeInHierarchy) {
                note.gameObject.SetActive(true);
            }

        }
    }

    void OnTriggerExit(Collider coll) {
        if(coll.CompareTag("Player")) {
            if (note.gameObject.activeInHierarchy) {
                note.gameObject.SetActive(false);
            }
        }
    }
}
