using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace BauhausRacer {
	public class GuiControllerGame : MonoBehaviour {

		[Header("Ingame")]
		public TextMeshProUGUI textTime;
		public TextMeshProUGUI textRounds;
		public RectTransform KMHNeedle;
		public GameObject pausePanel;
		
		//

		public Driving carController;

		[Header("Menu")]
		public GameObject menuPanel;

		public Text m_rankText;
		public Text m_nameText;
		public Text m_timeText;
		public GameObject controlsPanel;
		public GameObject creditsPanel;
		public ScrollRect highscoreScrollrect;


		[Header("Highscore")]
		public GameObject highScorePanel;
		
		public GameObject wheelInput;
		public GameObject keyboardInput;
		public InputField nameInput;
		public Text h_rankText;
		public Text h_nameText;
		public Text h_timeText;

		//Speed
		private float kmh = 0f;
		private float orgKMHNeedleAngle = 0f;
		//
		private enum ActiveScreen
		{
			MENU, CREDITS, CONTROLS, GAME, PAUSE, HIGHSCORE
		}

		private ActiveScreen activeScreen;

		private ArcadeHighscoreEntry arcadeHighscoreEntry;
		


		//menu is active, game is paused
		void Awake () {
			menuPanel.SetActive(true);
			pausePanel.SetActive(false);
			highScorePanel.SetActive(false);
			wheelInput.SetActive(false);
			keyboardInput.SetActive(false);
			creditsPanel.SetActive(false);
			controlsPanel.SetActive(false);
			orgKMHNeedleAngle = KMHNeedle.transform.localEulerAngles.z;
			activeScreen = ActiveScreen.MENU;
			
			LoadHighscore();
			PauseGame();
			
		}
		

		
		// Update is called once per frame
		void Update ()
        {
			if(!Game.Instance.gameStopped){
           		 textTime.text = "Time: " + GetMinutesDisplay(Game.Instance.timer);
			}

			
			// if(Input.GetAxis("DPadY")<0){
			// 	highscoreScrollrect.verticalScrollbar.value++;
			// }
           
			DisplaySpeed();
			
			switch (activeScreen) {
				case ActiveScreen.MENU:
					if(Input.GetButtonDown("Controls")){
						Controls();
					}
					if(Input.GetButtonDown("Credits")){
						Credits();
					}
					if(Input.GetButtonDown("Menu")){
						Exit();
					}
					if(Input.GetButtonDown("Play")){
						Play();
					}
				break;
				case ActiveScreen.CONTROLS:
					if(Input.GetButtonDown("Menu")){
						Back(controlsPanel);
					} 
					break;
				case ActiveScreen.CREDITS:
					if(Input.GetButtonDown("Menu")){
						Back(creditsPanel);
					} 
				break;
				case ActiveScreen.PAUSE:
					if(Input.GetButtonDown("Play")){
						Resume();
					}
					if(Input.GetButtonDown("Menu")){
						Menu();
					}
					if(Input.GetButtonDown("Controls")){
						Controls();
					}
				break;
				case ActiveScreen.GAME:
					if(Input.GetButtonDown("Pause")){
						PauseGame();
					}
					if(Input.GetButtonDown("Respawn")){
						Respawn_Player();
					}
				break;
				case ActiveScreen.HIGHSCORE:
					if(Input.GetButtonDown("Play")){
						arcadeHighscoreEntry = wheelInput.GetComponent<ArcadeHighscoreEntry>();
						arcadeHighscoreEntry.SubmitName();
						HighscoreEntry();
					}
				break;
			}

        }

		//Display Time in minutes and seconds
        public static string GetMinutesDisplay(float time){
			int minutes = (int)time / 60;
			int seconds = (int)time % 60; //rest von der Teilung
			return minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
		}

		//display rounds
		public void DisplayRounds(int currentRound){
			textRounds.text = currentRound.ToString()+"/"+Game.Instance.rounds.ToString();
		}

		//Display speed as kmh-needle
		public void DisplaySpeed(){
			if (!carController) {
					Debug.LogError ("Car is not selected on your UI Canvas " + gameObject.name);
					enabled = false;
					return;
			}

			kmh = carController.speed * 1.2f;

			Quaternion target = Quaternion.Euler (0f, 0f, orgKMHNeedleAngle - kmh);
			KMHNeedle.rotation = Quaternion.Slerp(KMHNeedle.rotation, target,  Time.deltaTime * 2f);
		}

		//Respawn player
		public void Respawn_Player(){
			CheckpointManager.Instance.ResetPlayerToCurrentCheckpoint();
		}

		//begin game
		public void Play(){
			menuPanel.SetActive(false);
			activeScreen = ActiveScreen.GAME;
			Resume();
		}

		//pause game
		public void PauseGame(){
			pausePanel.SetActive(true);
			activeScreen = ActiveScreen.PAUSE;
			Game.Instance.gameStopped = true; //timer mustn't count during pause
			Time.timeScale = 0f;
		}

		//resume game after pause
		public void Resume(){
			activeScreen = ActiveScreen.GAME;
			Time.timeScale = 1.5f;
			pausePanel.SetActive(false);
			Game.Instance.gameStopped= false;
		}

		//exit game
		public void Exit(){
			Application.Quit();
		}

		//back from controls or credits to menu or game
		public void Back(GameObject panel){
			panel.SetActive(false);
			if(menuPanel.activeSelf){
				activeScreen = ActiveScreen.MENU;
			} else {		//if controls is called from the paused game
				activeScreen = ActiveScreen.PAUSE;
			}
		}

		//show controls panel
		public void Controls(){
			//PauseGame(); 
			activeScreen = ActiveScreen.CONTROLS;
			controlsPanel.SetActive(true);
		}

		//show menu panel 
		public void Menu(){
			activeScreen =ActiveScreen.MENU;
			SceneManager.LoadScene(0); //load scene to reset everything and to reload highscore
		}

		//show credits panel
		public void Credits(){
			activeScreen = ActiveScreen.CREDITS;
			creditsPanel.SetActive(true);
		}

		//new entry in highscore database (if name is not empty)
		public void HighscoreEntry(){
			if(!Game.Instance.wheel){
				Game.Instance.PlayerName = nameInput.text;
			}
			if(Game.Instance.PlayerName!=""){
				XMLManager.instance.highscoreDatabase.AddEntry(Game.Instance.PlayerName, Game.Instance.timer);
				SceneManager.LoadScene(0);
			}
		}

		//wheel conntected, name entry via arcade-highscore-entry-system
		private void ShowWheelInput(){
			wheelInput.SetActive(true);
		}

		//no wheel conntected, name entry via keyboard
		private void ShowKeyboardInput(){
			keyboardInput.SetActive(true);
		}

		//display whole highscore in main menu
		public void LoadHighscore(){
				
			List<HighScoreEntry> highScoreEntries = XMLManager.instance.highscoreDatabase.list;
			int rank= 1;
			foreach(HighScoreEntry h in highScoreEntries){
				m_nameText.text += h.name+"\n";
				m_timeText.text += GuiControllerGame.GetMinutesDisplay(h.time)+"\n";
				m_rankText.text += rank.ToString()+"\n";
				rank++;
			}
		}
	
		//show highscore-Panel (when game is finished): display player's rank and the rank above and below his
		public void ShowHighscorePanel(){
			Game.Instance.gameStopped = true;
			if(Game.Instance.wheel){		//wheel or keyboard input
				ShowWheelInput();
			} else {
				ShowKeyboardInput();
			}
			highScorePanel.SetActive(true);

			XMLManager.instance.highscoreDatabase.AddEntry("", Game.Instance.timer); //add entry with empty name, to get the rank
		
			List<HighScoreEntry> highscore = XMLManager.instance.highscoreDatabase.list; //get the index and so the rank of the new entry
			int index = 0;
			for(int i=0; i<highscore.Count; i++){
				if(highscore[i].time == Game.Instance.timer){
					index = i;
					break;
				}
			}

			//show the current player's rank and time in highlighted field, show the entry above and below
			if(highscore.Count >=3){
				activeScreen = ActiveScreen.HIGHSCORE;
				if(index == highscore.Count-1){ 		//new entry is the last one
					h_rankText.text = index.ToString()+"\n"+ (index+1).ToString();
					h_nameText.text = highscore[index-1].name+"\n";
					h_timeText.text = GetMinutesDisplay(highscore[index-1].time)+"\n"+GetMinutesDisplay(highscore[index].time);
					XMLManager.instance.highscoreDatabase.RemoveEntry(index);
				} else {			
					h_rankText.text = index.ToString()+"\n"+ (index+1).ToString()+"\n"+(index+2).ToString();
					h_nameText.text = highscore[index-1].name+"\n"+highscore[index].name+"\n"+highscore[index+1].name;
					h_timeText.text = GetMinutesDisplay(highscore[index-1].time)+"\n"+GetMinutesDisplay(highscore[index].time)+"\n"+GetMinutesDisplay(highscore[index+1].time);
					XMLManager.instance.highscoreDatabase.RemoveEntry(index);
				}
			} else if (highscore.Count == 1){		//new entry is the first entry
				h_rankText.text = "\n"+index+1.ToString();
				h_nameText.text = "\n"+highscore[index].name;
				h_timeText.text = "\n"+GetMinutesDisplay(highscore[index].time);
				XMLManager.instance.highscoreDatabase.RemoveEntry(index);
			} else if (highscore.Count == 2){		//new entry is the second entry
				if(index == 1){						//2. place
					h_rankText.text = index.ToString()+ "\n" + (index+1).ToString();
					h_nameText.text = highscore[index-1].name+ "\n"+ highscore[index].name;
					h_timeText.text = GetMinutesDisplay(highscore[index-1].time)+ "\n"+ GetMinutesDisplay(highscore[index].time);
					XMLManager.instance.highscoreDatabase.RemoveEntry(index);
				} else if(index == 0){				//1. place
					h_rankText.text = "\n"+index+1.ToString() + "\n"+index+2.ToString();
					h_nameText.text = "\n"+highscore[index].name+ "\n"+highscore[index+1].name;
					h_timeText.text = "\n"+GetMinutesDisplay(highscore[index].time)+  "\n"+GetMinutesDisplay(highscore[index+1].time);
					XMLManager.instance.highscoreDatabase.RemoveEntry(index);
				}
				
			}
			
		}	
	}
}
