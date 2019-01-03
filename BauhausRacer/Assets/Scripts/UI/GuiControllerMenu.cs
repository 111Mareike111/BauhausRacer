using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BauhausRacer{
	public class GuiControllerMenu : MonoBehaviour {

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			if(Input.GetKeyDown(KeyCode.Joystick1Button6)){
				Play();
			}
			if(Input.GetKeyDown(KeyCode.Joystick1Button4)){
				Credits();
			}
			if(Input.GetKeyDown(KeyCode.Joystick1Button3)){
				Controls();
			}
			if(Input.GetKeyDown(KeyCode.Joystick1Button5)){
				Exit();
			}
		}

		public void Play(){
			SceneManager.LoadScene(1);
		}
		public void Credits(){
			SceneManager.LoadScene(3);
		}

		public void Controls(){
			SceneManager.LoadScene(2);
		}

		public void Exit(){
			Application.Quit();
		}

		public void Menu(){
			SceneManager.LoadScene(0);
		}
	}

}

