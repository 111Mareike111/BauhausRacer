using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BauhausRacer{
public class ArcadeHighscoreEntry : MonoBehaviour
{
        private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ.-";
        private int stepper = 0;
        private int letterSelect = 0;
        public Text[] Letters;
        public float moveDelayUpDown = 0.2f;
		public float moveDelayRightLeft = 1f;
        private bool readyToMove = false;

		public GameObject[] arrows;

		public string arrowColorHex;
		public string arrowHighlightColorHex;
		public string textColorHex;

		public bool isEnteringName = true;
		private Color arrowColor;
		Color textcolor;

		public Button okButton;
		public Button enterNameButton;
	//	private bool m_isAxisInUse = false;
 
        void Start ()
        {
                Invoke("ResetReadyToMove", 3f);
				Letters[letterSelect].text = alphabet[stepper].ToString ();
				 
				ColorUtility.TryParseHtmlString(textColorHex, out textcolor);
				ColorUtility.TryParseHtmlString(arrowHighlightColorHex, out arrowColor);
				Letters[letterSelect].color = textcolor;
        }

		public void SetIsEnteringName(bool boolean){
			isEnteringName = boolean;
		}
 
        void Update ()
        {
			if(isEnteringName){
				Color textcolor; 
				ColorUtility.TryParseHtmlString(textColorHex, out textcolor);
				Letters[letterSelect].color = textcolor;
				arrows[0].SetActive(true);
				arrows[1].SetActive(true);
                if (Input.GetKey ("up") && readyToMove || Input.GetAxis("DPadY")>0 && readyToMove || Input.GetButton("Forward") && readyToMove) {
                        if (stepper < alphabet.Length - 1) {
                                stepper++;
						}else{
							stepper = 0;
						}
						Letters [letterSelect].text = alphabet [stepper].ToString ();
						arrows[0].GetComponent<Image>().color = arrowColor;
						readyToMove = false;
						Invoke("ResetReadyToMove", moveDelayUpDown);
                }
                if (Input.GetKey ("down") && readyToMove || Input.GetAxis("DPadY")<0 && readyToMove || Input.GetButton("Backward") && readyToMove) {
                        if (stepper > 0) {
                                stepper--;
						}else{
							stepper = alphabet.Length-1;
						}
						Letters [letterSelect].text = alphabet [stepper].ToString ();
						arrows[1].GetComponent<Image>().color = arrowColor;
						readyToMove = false;
						Invoke ("ResetReadyToMove", moveDelayUpDown);
                }
                if (Input.GetKey("right") && readyToMove || Input.GetAxis("DPadX")>0 && readyToMove || Input.GetAxisRaw("Vertical2") > 0.6  && readyToMove) { //next Letter

		
 					
                    
							if (letterSelect < Letters.Length - 1) {
									letterSelect++;

									Letters[letterSelect].color = textcolor; // selected Letter is white
									if(Letters[letterSelect].text==""){
										stepper=0;
										Letters[letterSelect].text = alphabet[stepper].ToString();
									}							
									Letters [letterSelect - 1].color = Color.black;
									arrows[0].transform.position = new Vector3(arrows[0].transform.position.x+24f, arrows[0].transform.position.y, 0);
									arrows[1].transform.position = new Vector3(arrows[1].transform.position.x+24f, arrows[1].transform.position.y, 0);
									readyToMove = false;
									Invoke ("ResetReadyToMove", moveDelayRightLeft);
							} else {
								isEnteringName = false;
								enterNameButton.Select();
								okButton.Select();
								Letters[letterSelect].color = Color.black;
								arrows[0].SetActive(false);
								arrows[1].SetActive(false);
								okButton.onClick.Invoke();
								readyToMove = false;
								Invoke ("ResetReadyToMove", moveDelayRightLeft);
							}
					//	m_isAxisInUse = true;
					
                    
                }
				if (Input.GetKey("left") && readyToMove || Input.GetAxis("DPadX")<0 && readyToMove || Input.GetAxisRaw("Vertical2") < -0.6  && readyToMove) { //next Letter
                    
 					
							if (letterSelect > 0) {
									letterSelect--;
	
									Letters[letterSelect].color = textcolor; // selected Letter is white
							
									Letters [letterSelect + 1].color = Color.black;
									arrows[0].transform.position = new Vector3(arrows[0].transform.position.x-24f, arrows[0].transform.position.y, 0);
									arrows[1].transform.position = new Vector3(arrows[1].transform.position.x-24f, arrows[1].transform.position.y, 0);
									readyToMove = false;
									Invoke ("ResetReadyToMove", moveDelayRightLeft);
							}
						//m_isAxisInUse = true;
					
                        
                }
				
			}	else {
				if (Input.GetKey("left") && readyToMove || Input.GetAxis("DPadX")<0 && readyToMove) {
					isEnteringName = true;
					Letters[letterSelect].color = textcolor;
					arrows[0].SetActive(true);
					arrows[1].SetActive(true);
					enterNameButton.Select();
					readyToMove = false;
					Invoke ("ResetReadyToMove", moveDelayRightLeft);
				}
				
			}	

        }
 
        void ResetReadyToMove ()
        {
            readyToMove = true;
			Color arrowNormalColor;
			ColorUtility.TryParseHtmlString(arrowColorHex, out arrowNormalColor);
			arrows[0].GetComponent<Image>().color = arrowNormalColor;
			arrows[1].GetComponent<Image>().color = arrowNormalColor;
        }

		public void SubmitName(){
			string name="";
			for(int i = 0; i<Letters.Length; i++){
				name += Letters[i].text.ToString();
			}
			Game.Instance.PlayerName = name;
			Debug.Log("name: "+name);
			XMLManager.instance.highscoreDatabase.AddEntry(Game.Instance.PlayerName, Game.Instance.timer);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		}


	
	}
}

