﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer {
    public class CheckpointManager : MonoBehaviour {

        public static CheckpointManager Instance;

        public GuiController guiController;

        private Checkpoint _currentCheckpoint;
        private Checkpoint _nextCheckpoint;
        private Checkpoint _lastCheckpoint;

        private bool wrongDirection = true;
        public GameObject turnBack_img;

        public Checkpoint NextCheckpoint { get { return _nextCheckpoint; } }
        public Checkpoint LastCheckpoint { get { return _currentCheckpoint; } }

        private int checkpointIndex;

        [SerializeField] private Transform _playerTranform;
        [SerializeField] private Material materialNextCheckpoint;
        [SerializeField] private Material materialOtherCeckpoint;
        [SerializeField] private Material materialLastCeckpoint;
        private int _currentRound;
        private Checkpoint[] checkpoints;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            turnBack_img.SetActive(false);
        }

        // Use this for initialization
        void Start () {
            if(_playerTranform == null)
            {
                Debug.LogError("Missing Refernece: In the CheckpointManager is no Car");
            }
            checkpointIndex = 0;
            _currentRound = 1;
            checkpoints = gameObject.GetComponentsInChildren<Checkpoint>();
            _currentCheckpoint = null;
            _nextCheckpoint = checkpoints[checkpointIndex];
            _lastCheckpoint = checkpoints[checkpoints.Length-1];
            visualAid();

        }

        public void NextCheckpointArrived()
        {
            checkpointIndex++;
            Debug.Log("checkpointIndex " + checkpointIndex);
            if (checkpointIndex == checkpoints.Length-1)
            {
                _currentCheckpoint = checkpoints[checkpointIndex];
                _nextCheckpoint = checkpoints[0];
                _lastCheckpoint = checkpoints[checkpointIndex - 1];
            }
            else if (checkpointIndex == checkpoints.Length)
            {
                if(_currentRound == Game.Instance.rounds){
                    Game.Instance.EndGame();
                    guiController.showFinish();
                    return;
                }
                _currentRound++;
                guiController.DisplayRounds(_currentRound);
                checkpointIndex = 0;
                _currentCheckpoint = checkpoints[checkpointIndex];
                _nextCheckpoint = checkpoints[checkpointIndex + 1];
                _lastCheckpoint = checkpoints[checkpoints.Length-1];
      
            }
            else
            {
                _currentCheckpoint = checkpoints[checkpointIndex];
                _nextCheckpoint = checkpoints[checkpointIndex + 1];
                _lastCheckpoint = checkpoints[checkpointIndex - 1];
            }
            visualAid();
            //Debug.Log(_currentRound);
        }

        public void ResetPlayerToCurrentCheckpoint()
        {
            _playerTranform = _currentCheckpoint.SpawnPosition;
        }
        
        private void visualAid()
        {
            _nextCheckpoint.GetComponent<Renderer>().material = materialNextCheckpoint;
            _currentCheckpoint.GetComponent<Renderer>().material = materialOtherCeckpoint;
            _lastCheckpoint.GetComponent<Renderer>().material = materialLastCeckpoint;
        }

        public void WrongDirection()
        {
            if (wrongDirection)
            {
                wrongDirection = false;
                turnBack_img.SetActive(false);
            } else
            {
                wrongDirection = true;
                turnBack_img.SetActive(true);
                Debug.Log("wrong direction");
            }
            
        }

    }
}