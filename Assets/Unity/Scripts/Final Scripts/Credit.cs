using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credit : MonoBehaviour {

	public GameObject CreditPage;
    public void Clicked(){
          CreditPage.SetActive(true); 
    }
    public void Exit(){
          CreditPage.SetActive(false); 
    }
}
