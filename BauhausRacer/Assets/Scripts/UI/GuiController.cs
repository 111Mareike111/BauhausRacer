using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace BauhausRacer {
	public class GuiController : MonoBehaviour {
		public TextMeshProUGUI textTime;

		public TextMeshProUGUI textRounds;
		public Driving carController;
		public GameObject finishPanel;

		public GameObject pausePanel;

		public GameObject highScorePanel;
		public GameObject wheelInput;
		public GameObject keyboardInput;

		public InputField nameInput;

		[Header("Speed")]
		private float kmh = 0f;

		public RectTransform KMHNeedle;

		private float orgKMHNeedleAngle = 0f;

		//
		private float timer;


		// Use this for initialization
		void Awake () {
			timer = 0f;
			finishPanel.SetActive(false);
			pausePanel.SetActive(false);
			highScorePanel.SetActive(false);
			wheelInput.SetActive(false);
			keyboardInput.SetActive(false);
			orgKMHNeedleAngle = KMHNeedle.transform.localEulerAngles.z;
			Time.timeScale=1f;
		}
		
		// Update is called once per frame
		void Update ()
        {
			if(!Game.Instance.gameStopped){
				 timer += Time.deltaTime;
           		 textTime.text = "Time: " + GetMinutesDisplay(timer);
			}
           
			DisplaySpeed();
			if(Input.GetKeyDown(KeyCode.Joystick1Button15)){
				PauseGame();
			}
			if(Input.GetKeyDown(KeyCode.Joystick1Button14)){
				Resume();
			}
			if(Input.GetKeyDown(KeyCode.Joystick1Button12)){
				
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

		public void showFinish(){
			finishPanel.SetActive(true);
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
			Time.timeScale = 1f;
			pausePanel.SetActive(false);
		}

		public void Controls(){
			SceneManager.LoadScene(2);
		}
		public void Menu(){
			SceneManager.LoadScene(0);
		}

		public void HighscoreEntry(){
			Game.Instance.PlayerName = nameInput.text;
			SceneManager.LoadScene(0);
		}

		public void ShowHighscorePanel(){
			Game.Instance.gameStopped = true;
			if(true){
				ShowWheelInput();
			} else {
				ShowKeyboardInput();
			}
			highScorePanel.SetActive(true);
			
		}

		private void ShowWheelInput(){
			wheelInput.SetActive(true);
		}

		private void ShowKeyboardInput(){
			keyboardInput.SetActive(true);
		}
		
		
	}
}
