using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BauhausRacer {
	public class GuiController : MonoBehaviour {
		public TextMeshProUGUI textTime;

		public TextMeshProUGUI textRounds;
		public Driving carController;
		public GameObject finishPanel;

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
			orgKMHNeedleAngle = KMHNeedle.transform.localEulerAngles.z;
		}
		
		// Update is called once per frame
		void Update ()
        {
            timer += Time.deltaTime;
            textTime.text = "Time: " + GetMinutesDisplay(timer);
			DisplaySpeed();
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
	}
}
