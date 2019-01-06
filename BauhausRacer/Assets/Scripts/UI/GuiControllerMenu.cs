using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BauhausRacer{
	public class GuiControllerMenu : MonoBehaviour {

		public Text placeText;
		public Text nameText;
		public Text timeText;

	
		// Use this for initialization
		void Start () {
			LoadHighscore();
		}
		
		// Update is called once per frame
		void Update () {
			if(Input.GetButtonDown("Play")){
				Debug.Log("p");
				Play();
			}
			if(Input.GetButtonDown("Credits")){
				Credits();
			}
			if(Input.GetButtonDown("Controls")){
				Controls();
			}
			if(Input.GetButtonDown("Menu")){
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

		public void LoadHighscore(){
			
			List<HighScoreEntry> highScoreEntries = XMLManager.instance.highscoreDatabase.list;
			int count = 1;
			foreach(HighScoreEntry h in highScoreEntries){
				nameText.text += h.name+"\n";
				timeText.text += GuiControllerGame.GetMinutesDisplay(h.time)+"\n";
				placeText.text += count.ToString()+"\n";
				count++;
			}
		}
	}

}

