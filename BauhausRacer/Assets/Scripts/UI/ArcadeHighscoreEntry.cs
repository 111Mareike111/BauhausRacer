using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BauhausRacer{
public class ArcadeHighscoreEntry : MonoBehaviour
{
        private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ.-";
        private int stepper = 0;
        private int letterSelect = 0;
        public Text[] Letters = null;
        public float moveDelay = 0.2f;
        private bool readyToMove = true;

		public GameObject arrows;
 
        void Start ()
        {
                Letters [letterSelect].text = alphabet [stepper].ToString ();
				Letters[letterSelect].color = Color.white;
        }
 
        void Update ()
        {
                if (Input.GetKey ("up") && readyToMove) {
                        if (stepper < alphabet.Length - 1) {
                                stepper++;
						}else{
							stepper = 0;
						}
						Letters [letterSelect].text = alphabet [stepper].ToString ();
						readyToMove = false;
						Invoke("ResetReadyToMove", moveDelay);
                }
                if (Input.GetKey ("down") && readyToMove) {
                        if (stepper > 0) {
                                stepper--;
						}else{
							stepper = alphabet.Length-1;
						}
						Letters [letterSelect].text = alphabet [stepper].ToString ();
						readyToMove = false;
						Invoke ("ResetReadyToMove", moveDelay);
                }
                if (Input.GetKey("right") && readyToMove) { //next Letter
                    
					if (letterSelect < Letters.Length - 1) {
							letterSelect++;
							Letters [letterSelect].color = Color.white; // selected Letter is white
							if(Letters[letterSelect].text==""){
								stepper=0;
								Letters[letterSelect].text = alphabet[stepper].ToString();
							}							
							Letters [letterSelect - 1].color = Color.black;
							arrows.transform.position = new Vector3(arrows.transform.position.x+18.7f, arrows.transform.position.y, 0);
							readyToMove = false;
							Invoke ("ResetReadyToMove", moveDelay+1);
					}
                        
                }
				if (Input.GetKey("left") && readyToMove) { //next Letter
                    
					if (letterSelect > 0) {
							letterSelect--;
							Letters [letterSelect].color = Color.white; // selected Letter is white
							
							Letters [letterSelect + 1].color = Color.black;
							arrows.transform.position = new Vector3(arrows.transform.position.x-18.7f, arrows.transform.position.y, 0);
							readyToMove = false;
							Invoke ("ResetReadyToMove", moveDelay+1);
					}
                        
                }

        }
 
        void ResetReadyToMove ()
        {
            readyToMove = true;
        }

		public void SubmitName(){
			string name="";
			for(int i = 0; i<Letters.Length; i++){
				name += Letters[i].text.ToString();
			}
			Game.Instance.PlayerName = name;
			Debug.Log("name: "+name);
		}
	}
}

