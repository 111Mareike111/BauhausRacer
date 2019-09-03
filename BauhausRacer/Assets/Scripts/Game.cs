using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace BauhausRacer
{
    public enum SelectColor {Red, Yellow, Blue, Orange, Green, Violet};
    public class Game : MonoBehaviour
    {
        public static Game Instance = null;
        public ColorManager ColorManager { get; private set; }

        public GameObject carBody;
           public GameObject wrongColorText;
        [Header("Audio")]
        public AudioMixer IngameAudio;
        
        public float volumeIngame = 0f;
        public GameObject Music;
        [HideInInspector]public AudioSource _musicIngame;
        [HideInInspector]public AudioSource _musicMenu;

        public float horizontal;
        public float vertical;

        public int rounds =3;
        public float waitingdelay = 60f;
        public string PlayerName {get; set;}

        public bool gameStopped {get; set;}

        public float timer {get;set;}

        public float timerResetGame {get;set;} //timer for resetting game when no input
        public bool wheel{get;set;}

        public bool CameraStart {get;set;}

     


        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            } else if(Instance != this)
            {
                Destroy(gameObject);
            }

            ColorManager = GetComponent<ColorManager>();

            
            wheel = false;

            for(int i = 0; i<Input.GetJoystickNames().Length; i++){
                if(Input.GetJoystickNames()[i].Equals("B677") || Input.GetJoystickNames()[i].Equals("Thrustmaster Racing Wheel FFB")){
                    wheel = true;
                    break;
                }
            }
            _musicIngame = Music.GetComponents<AudioSource>()[0];
            _musicMenu = Music.GetComponents<AudioSource>()[1];
        }

        // Use this for initialization
        void Start()
        {
            string[] names = Input.GetJoystickNames();
            Debug.Log("Connected Joysticks:");
            for(int i = 0; i < names.Length; i++) {
                Debug.Log("Joystick" + (i + 1) + " = " + names[i]);
            }
            CameraStart = false;
            timerResetGame = Time.realtimeSinceStartup + waitingdelay;
        }

        // Update is called once per frame
        void Update()
        {
            horizontal = Input.GetAxis("Horizontal2");
            vertical = Input.GetAxis("Vertical2");
           if(!gameStopped){
              timer += (Time.deltaTime/1.5f); 
              Cursor.visible = false;
           } else {
               Cursor.visible = true;
           }

           
                   
            if(carBody.transform.position.y < 218){
                CheckpointManager.Instance.ResetPlayerToCurrentCheckpoint();
            }  

            if (Input.anyKey || Input.GetAxis("DPadY") != 0 || Input.GetAxis("DPadX") != 0 || horizontal != 0 || vertical != 0)
                SetInitiolSituation();
            else
                if(!gameStopped){
                    CheckTimer();
                }
        }

        public void EndGame(){
            Debug.Log("Ende");
        }

         void SetInitiolSituation()
            {
                timerResetGame = Time.realtimeSinceStartup + waitingdelay;
                
            }

            void CheckTimer()
            {
                if (timerResetGame < 0)
                {
                    ResetGame();
                }
                else
                {
                    if(Time.realtimeSinceStartup > timerResetGame)
                    {
                        ResetGame();
                    }
                    
                }
            }

            void ResetGame(){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            }


    }

   
}

