﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BauhausRacer {
	public class GuiController : MonoBehaviour {
		public TextMeshProUGUI textTime;
		public TextMeshProUGUI textSpeed;

		public TextMeshProUGUI textRounds;
		public MyCarController carController;
		public GameObject finishPanel;
		//
		private float timer;


		// Use this for initialization
		void Awake () {
			timer = 0f;
			finishPanel.SetActive(false);
		}
		
		// Update is called once per frame
		void Update ()
        {
            timer += Time.deltaTime;
            textTime.text = "Time: " + GetMinutesDisplay(timer);

            textSpeed.text = GetSpeedDisplay();
        }

		// get rounded speed and speedtype
		public string GetSpeedDisplay(){
		//	return (Mathf.Round(carController.currentSpeed)).ToString() + " kph";
		return "";
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
	}
}
