using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BauhausRacer{
	public class GuiControllerCreditsControls : MonoBehaviour {
		
	
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Menu")){
			Menu();
		}
	}

	public void Menu(){
		SceneManager.LoadScene(0);
	}
}
}

