using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace BauhausRacer {
	public class GuiControllerGame : MonoBehaviour {
		public TextMeshProUGUI textTime;
		public TextMeshProUGUI textRounds;
		
		//

		public Driving carController;

		[Header("Panels")]

		public GameObject pausePanel;

		public GameObject wheelInput;
		public GameObject keyboardInput;

		public InputField nameInput;

		[Header("Highscore")]

		public GameObject highScorePanel;
		public Text rankText;
		public Text nameText;
		public Text timeText;

		[Header("Speed")]
		private float kmh = 0f;

		public RectTransform KMHNeedle;

		private float orgKMHNeedleAngle = 0f;

		//


		// Use this for initialization
		void Awake () {
			pausePanel.SetActive(false);
			highScorePanel.SetActive(false);
			wheelInput.SetActive(false);
			keyboardInput.SetActive(false);
			orgKMHNeedleAngle = KMHNeedle.transform.localEulerAngles.z;
			
		}
		
		// Update is called once per frame
		void Update ()
        {
			if(!Game.Instance.gameStopped){
           		 textTime.text = "Time: " + GetMinutesDisplay(Game.Instance.timer);
			}
           
			DisplaySpeed();
			if(Input.GetButtonDown("Pause")){
				PauseGame();
			}
			if(Input.GetButtonDown("Play")){
				Resume();
			}
			if(Input.GetButtonDown("Menu")){
				Menu();
			}
			if(Input.GetButtonDown("Controls")){
				Controls();
			}

			if(highScorePanel.activeSelf){
				if(Input.GetButtonDown("Play")){
					HighscoreEntry();
				}
			}
        }

		//Display Time in minutes and seconds
        public static string GetMinutesDisplay(float time){
			int minutes = (int)time / 60;
			int seconds = (int)time % 60; //rest von der Teilung
			return minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
		}


		public void DisplayRounds(int currentRound){
			textRounds.text = currentRound.ToString()+"/"+Game.Instance.rounds.ToString();
		}


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

		public void Respawn_Player(){
			CheckpointManager.Instance.ResetPlayerToCurrentCheckpoint();
		}
 
		public void PauseGame(){
			pausePanel.SetActive(true);
			Time.timeScale = 0f;
		}

		public void Resume(){
			Time.timeScale = 1.5f;
			pausePanel.SetActive(false);
		}

		public void Controls(){
			SceneManager.LoadScene(2);
		}
		public void Menu(){
			SceneManager.LoadScene(0);
		}

		public void HighscoreEntry(){
		}

		private void SubmitName(){

		}

	

		public void ShowHighscorePanel(){
			Game.Instance.gameStopped = true;
			if(true){
				ShowWheelInput();
			} else {
				ShowKeyboardInput();
			}
			highScorePanel.SetActive(true);
			XMLManager.instance.highscoreDatabase.AddEntry("", Game.Instance.timer);
		
			List<HighScoreEntry> highscore = XMLManager.instance.highscoreDatabase.list;
			int index = 0;
			for(int i=0; i<highscore.Count; i++){
				if(highscore[i].time == Game.Instance.timer){
					index = i;
					break;
				}
			}
			Debug.Log("i: "+index.ToString()+" C: "+highscore.Count);
			if(highscore.Count >=3){
				if(index == highscore.Count-1){
					rankText.text = index.ToString()+"\n"+ (index+1).ToString();
					nameText.text = highscore[index-1].name+"\n";
					timeText.text = GetMinutesDisplay(highscore[index-1].time)+"\n"+GetMinutesDisplay(highscore[index].time);
					XMLManager.instance.highscoreDatabase.RemoveEntry(index);


				} else {
					rankText.text = index.ToString()+"\n"+ (index+1).ToString()+"\n"+(index+2).ToString();
					nameText.text = highscore[index-1].name+"\n"+highscore[index].name+"\n"+highscore[index+1].name;
					timeText.text = GetMinutesDisplay(highscore[index-1].time)+"\n"+GetMinutesDisplay(highscore[index].time)+"\n"+GetMinutesDisplay(highscore[index+1].time);
					XMLManager.instance.highscoreDatabase.RemoveEntry(index);
				}
				
				
			} else if (highscore.Count == 1){
				rankText.text = "\n"+index+1.ToString();
				nameText.text = "\n"+highscore[index].name;
				timeText.text = "\n"+GetMinutesDisplay(highscore[index].time);
				XMLManager.instance.highscoreDatabase.RemoveEntry(index);
			} else if (highscore.Count == 2){
				if(index == 1){
					rankText.text = index.ToString()+ "\n" + (index+1).ToString();
					nameText.text = highscore[index-1].name+ "\n"+ highscore[index].name;
					timeText.text = GetMinutesDisplay(highscore[index-1].time)+ "\n"+ GetMinutesDisplay(highscore[index].time);
					XMLManager.instance.highscoreDatabase.RemoveEntry(index);
				} else if(index == 0){
					rankText.text = "\n"+index+1.ToString() + "\n"+index+2.ToString();
					nameText.text = "\n"+highscore[index].name+ "\n"+highscore[index+1].name;
					timeText.text = "\n"+GetMinutesDisplay(highscore[index].time)+  "\n"+GetMinutesDisplay(highscore[index+1].time);
					XMLManager.instance.highscoreDatabase.RemoveEntry(index);
				}
				
			}
			
		}

		private void ShowWheelInput(){
			wheelInput.SetActive(true);
		}

		private void ShowKeyboardInput(){
			keyboardInput.SetActive(true);
		}
		
		
	}
}
