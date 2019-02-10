using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace BauhausRacer {
	public class GuiControllerGame : MonoBehaviour {

		[Header("Ingame")]
		public GameObject ingameUI;
		public TextMeshProUGUI textTime;
		public Image[] roundDisplay;
		public IlluminaitonBehaviour[] illuminaitonBehaviours;
		public Sprite roundSprite;
		public RectTransform KMHNeedle;
		public GameObject pausePanel;

		public Image[] carColorDisplay;
		
		
		//

		public UnityStandardAssets.Vehicles.Car.CarController carController;

		[Header("Menu")]
		public GameObject menuPanel;

		public GameObject rankPrefab;
		public GameObject timePrefab;
		public GameObject namePrefab;
		public GameObject grid;
		public GameObject controlsPanel;
		public GameObject creditsPanel;
		public AudioSource buttonClickAudio;
		public AudioSource playButtonClickAudio;


		[Header("Manual")]
		public ScrollRect scrollRect;
		public RectTransform[] manualCards;
		private int manualIndex = 0;
		private bool readyToMove = true;
		private float moveDelay = 0.3f;

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
		public enum ActiveScreen
		{
			MENU, CREDITS, CONTROLS, GAME, PAUSE, HIGHSCORE, INTRO
		}

		public bool isInMainMenu = true;

		private ActiveScreen activeScreen;

		private ArcadeHighscoreEntry arcadeHighscoreEntry;
		
		private Color red, blue, yellow, orange, green, violet;
		private Color blue_h, red_h, yellow_h, orange_h, green_h, violet_h;


		//menu is active, game is paused
		void Awake () {
			menuPanel.SetActive(true);
			Game.Instance._musicMenu.Play();
			highScorePanel.SetActive(false);
			wheelInput.SetActive(false);
			keyboardInput.SetActive(false);
			creditsPanel.SetActive(false);
			controlsPanel.SetActive(false);
			orgKMHNeedleAngle = KMHNeedle.transform.localEulerAngles.z;
			PauseGame();
			pausePanel.SetActive(false);
			Time.timeScale = 1f;
			activeScreen = ActiveScreen.MENU;
			buttonClickAudio.Stop();
			isInMainMenu = true;
			Debug.Log(isInMainMenu);
			LoadHighscore();
			
			
			ColorUtility.TryParseHtmlString("#92A1E7", out blue);
			ColorUtility.TryParseHtmlString("#2339E7", out blue_h);
			ColorUtility.TryParseHtmlString("#FF7E7E", out red);
			ColorUtility.TryParseHtmlString("#FF1B11", out red_h);
			ColorUtility.TryParseHtmlString("#FFF068", out yellow);
			ColorUtility.TryParseHtmlString("#FFE516", out yellow_h);
			ColorUtility.TryParseHtmlString("#83E78E", out green);
			ColorUtility.TryParseHtmlString("#16E708", out green_h);
			ColorUtility.TryParseHtmlString("#AE87E7", out violet);
			ColorUtility.TryParseHtmlString("#BD27E7", out violet_h);
			ColorUtility.TryParseHtmlString("#FFAB6C", out orange);
			ColorUtility.TryParseHtmlString("#FF7600", out orange_h);
		}
		

		
		// Update is called once per frame
		void Update ()
        {
			if(!Game.Instance.gameStopped){
           		 textTime.text = GetMinutesDisplay(Game.Instance.timer);
			}

		
			// if(Input.GetAxis("DPadY")<0){
			// 	highscoreScrollrect.verticalScrollbar.value++;
			// }

           
			DisplaySpeed();
			DisplayCarColor();

			switch (activeScreen) {
				case ActiveScreen.MENU:
					isInMainMenu = true;
					if(Input.GetButtonDown("Controls")||Input.GetKeyDown(KeyCode.M)){
						Controls();
					}
					if(Input.GetButtonDown("Credits")||Input.GetKeyDown(KeyCode.C)){
						Credits();
					}
					if(Input.GetButtonDown("Menu")||Input.GetKeyDown(KeyCode.Escape)){
						Exit();
					}
					if(Input.GetButtonDown("Play")||Input.GetKeyDown(KeyCode.Return)){
						Play();
						
					}
				break;
				case ActiveScreen.CONTROLS:
					isInMainMenu = true;
					if(Input.GetButtonDown("Menu")||Input.GetKeyDown(KeyCode.Backspace)){
						Back(controlsPanel);
					} 
					if(Input.GetAxis("DPadX")>0 && readyToMove){
						NextManualCard();
						readyToMove = false;
						Invoke("ResetReadyToMove", moveDelay);
					} else if(Input.GetAxis("DPadX")<0 && readyToMove){
						PreviousManualCard();
						readyToMove = false;
						Invoke("ResetReadyToMove", moveDelay);
					}
					break;
				case ActiveScreen.CREDITS:
					isInMainMenu = true;
					if(Input.GetButtonDown("Menu")||Input.GetKeyDown(KeyCode.Backspace)){
						Back(creditsPanel);
					} 
				break;
				case ActiveScreen.PAUSE:
					isInMainMenu = false;
					if(Input.GetButtonDown("Play")||Input.GetKeyDown(KeyCode.Return)){
						Resume();
					}
					if(Input.GetButtonDown("Menu")||Input.GetKeyDown(KeyCode.Backspace)){
						Menu();
					}
					if(Input.GetButtonDown("Controls")||Input.GetKeyDown(KeyCode.M)){
						Controls();
					}
				break;
				case ActiveScreen.GAME:
					isInMainMenu = false;
					if(Input.GetButtonDown("Pause")||Input.GetKeyDown(KeyCode.Escape)){
						PauseGame();
					}
					if(Input.GetButtonDown("Respawn")||Input.GetKeyDown(KeyCode.R)){
						Respawn_Player();
					}
				break;
				case ActiveScreen.HIGHSCORE:
					
					isInMainMenu = false;
					if(Input.GetButtonDown("Play") || Input.GetKeyDown(KeyCode.Return)){
						arcadeHighscoreEntry = wheelInput.GetComponent<ArcadeHighscoreEntry>();
						arcadeHighscoreEntry.SubmitName();
						HighscoreEntry();
					}
				break;
				case ActiveScreen.INTRO:
					Cursor.visible = false;
				break;
			}
			
			Debug.Log(activeScreen);

        }
		
		void ResetReadyToMove(){
			readyToMove = true;
		}

		public ActiveScreen GetActiveScreen(){
			return activeScreen;
		}

		public void DisplayCarColor(){
			switch(Game.Instance.ColorManager.CurrentColor.ColorName){
				case "Red":
					carColorDisplay[0].color = red_h;
					carColorDisplay[1].color = Color.white;
					carColorDisplay[2].color = Color.white;
					break;

				case "Yellow":
					carColorDisplay[1].color = yellow_h;
					carColorDisplay[0].color = Color.white;
					carColorDisplay[2].color = Color.white;
					break;

				case "Blue":
					carColorDisplay[1].color = Color.white;
					carColorDisplay[2].color = Color.white;
					carColorDisplay[0].color = blue_h;
					break;

				case "Orange":
					carColorDisplay[0].color = red;
					carColorDisplay[1].color = yellow;
					carColorDisplay[2].color = orange_h;
					break;

				case "Green":
					carColorDisplay[0].color = blue;
					carColorDisplay[2].color = green_h;
					carColorDisplay[1].color = yellow;
					break;

				case "Violet":
					if(carColorDisplay[0].color == red_h){
						carColorDisplay[0].color = red;
						carColorDisplay[1].color = blue;
					} else {
						carColorDisplay[1].color = red;
						carColorDisplay[0].color = blue;
					}
	
					carColorDisplay[2].color = violet_h;
					break;
				case "NoColor":
					carColorDisplay[0].color = Color.white;
					carColorDisplay[1].color = Color.white;
					carColorDisplay[2].color = Color.white;
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
			roundDisplay[currentRound-1].sprite = roundSprite;
			illuminaitonBehaviours[currentRound-1].GlowMaterial(true);
			
		}

		//Display speed as kmh-needle
		public void DisplaySpeed(){
			if (!carController) {
					Debug.LogError ("Car is not selected on your UI Canvas " + gameObject.name);
					enabled = false;
					return;
			}

			kmh = carController.CurrentSpeed * 1.2f;

			Quaternion target = Quaternion.Euler (0f, 0f, orgKMHNeedleAngle - kmh);
			KMHNeedle.rotation = Quaternion.Slerp(KMHNeedle.rotation, target,  Time.deltaTime * 1f);
		}

		//Respawn player
		public void Respawn_Player(){
			CheckpointManager.Instance.ResetPlayerToCurrentCheckpoint();
		}

		//begin game
		public void Play(){
			playButtonClickAudio.Play();
			menuPanel.SetActive(false);
			activeScreen = ActiveScreen.INTRO;
			Time.timeScale = 1.5f;
			Game.Instance._musicMenu.Stop();
			Game.Instance.CameraStart = true;
			
		}

		//pause game
		public void PauseGame(){
			buttonClickAudio.Play();
			pausePanel.SetActive(true);
			activeScreen = ActiveScreen.PAUSE;
			Game.Instance.gameStopped = true; //timer mustn't count during pause
			Time.timeScale = 0f;	
		
			Game.Instance._musicIngame.Pause();	
			Game.Instance.IngameAudio.SetFloat("Volume", -80f);		
		}

		//resume game after pause
		public void Resume(){
			if(!playButtonClickAudio.isPlaying){
				buttonClickAudio.Play();
			}
		
			activeScreen = ActiveScreen.GAME;
			Time.timeScale = 1.5f;
			pausePanel.SetActive(false);
			Game.Instance.gameStopped= false;

			Game.Instance.IngameAudio.SetFloat("Volume", Game.Instance.volumeIngame);
			if(!Game.Instance._musicIngame.isPlaying){
				Game.Instance._musicIngame.Play();
			}
		}

		//exit game
		public void Exit(){
			buttonClickAudio.Play();
			Application.Quit();
		}

		//back from controls or credits to menu or game
		public void Back(GameObject panel){
			buttonClickAudio.Play();
			panel.SetActive(false);
			if(pausePanel.activeSelf){
				activeScreen = ActiveScreen.PAUSE;
			} else {		
				activeScreen = ActiveScreen.MENU;
				menuPanel.SetActive(true);
			}
		}

		//show controls panel
		public void Controls(){
			//PauseGame(); 
			buttonClickAudio.Play();
			activeScreen = ActiveScreen.CONTROLS;
			controlsPanel.SetActive(true);
			menuPanel.SetActive(false);
            scrollRect.horizontalNormalizedPosition = 0;
			if(pausePanel.activeSelf){
				Game.Instance._musicMenu.Stop();
			}
		}

		//show menu panel 
		public void Menu(){
			//activeScreen = ActiveScreen.MENU;
			buttonClickAudio.Play();
			
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single); //load scene to reset everything and to reload highscore
		}

		//show credits panel
		public void Credits(){
			buttonClickAudio.Play();
			activeScreen = ActiveScreen.CREDITS;
			creditsPanel.SetActive(true);
			menuPanel.SetActive(false);
		}

		//new entry in highscore database (if name is not empty)
		public void HighscoreEntry(){
			buttonClickAudio.Play();
			if(!Game.Instance.wheel){
				Game.Instance.PlayerName = nameInput.text.ToUpper();
			}
			if(Game.Instance.PlayerName!=""){
				XMLManager.instance.highscoreDatabase.AddEntry(Game.Instance.PlayerName, Game.Instance.timer);
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
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
				GameObject entry = Instantiate(rankPrefab, grid.transform);
				entry.transform.SetParent(grid.transform);
				entry.GetComponent<Text>().text = rank.ToString();
				rank++;
				entry = Instantiate(timePrefab, grid.transform);
				entry.transform.SetParent(grid.transform);
				entry.GetComponent<Text>().text = GuiControllerGame.GetMinutesDisplay(h.time);

				entry = Instantiate(namePrefab, grid.transform);
				entry.transform.SetParent(grid.transform);
				entry.GetComponent<Text>().text = h.name;

				/* m_nameText.text += h.name+"\n";
				m_timeText.text += GuiControllerGame.GetMinutesDisplay(h.time)+"\n";
				m_rankText.text += rank.ToString()+"\n";
				rank++; */
			}
		}
	
		public void NextManualCard(){
			manualIndex++;
			if(manualIndex == manualCards.Length){
				manualIndex = 0;
			}
        
			scrollRect.horizontalNormalizedPosition = (float)manualIndex/((float)manualCards.Length-1.08f);
		}

		public void PreviousManualCard(){
			manualIndex--;
			if(manualIndex < 0){
				manualIndex = manualCards.Length-1;
			}
           scrollRect.horizontalNormalizedPosition = (float)manualIndex/((float)manualCards.Length-1.08f);
		}

		//show highscore-Panel (when game is finished): display player's rank and the rank above and below his
		public void ShowHighscorePanel(){
			Game.Instance.gameStopped = true;
			Game.Instance.IngameAudio.SetFloat("Volume", -80f);		
			if(Game.Instance.wheel){		//wheel or keyboard input
				ShowWheelInput();
			} else {
				ShowKeyboardInput();
			}
			highScorePanel.SetActive(true);
			activeScreen = ActiveScreen.HIGHSCORE;
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
