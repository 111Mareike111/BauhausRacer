using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer {
    public class CheckpointManager : MonoBehaviour {

        public static CheckpointManager Instance;

        public GuiControllerGame guiController;

        private Checkpoint _currentCheckpoint;
        private Checkpoint[] _nextCheckpoints;
        [SerializeField] private Checkpoint _firstCheckpiont;
        [SerializeField] private Checkpoint _lastCheckpoint;

        private bool wrongDirection = false;
        public GameObject turnBack_img;

        public Checkpoint[] NextCheckpoints { get { return _nextCheckpoints; } }
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
            _nextCheckpoints = new Checkpoint[1];
            _nextCheckpoints[0] = _firstCheckpiont;
            visualAid(true);

        }

        public void NextCheckpointArrived(Checkpoint currentCheckpoint, Checkpoint[] nextCheckpoints)
        {
            _currentCheckpoint = currentCheckpoint;
            if(_currentCheckpoint == _lastCheckpoint)
            {
                //Todo Increment course rounds
            }
            foreach(Checkpoint cp in _nextCheckpoints)
            {
                cp.SetNextCheckpoint(false);
            }
            visualAid(false);
            _nextCheckpoints = nextCheckpoints;
            visualAid(true);
            foreach (Checkpoint cp in _nextCheckpoints)
            {
                cp.SetNextCheckpoint(true);
            }
        }

        public void ResetPlayerToCurrentCheckpoint()
        {
            _playerTranform.position = _currentCheckpoint.SpawnPosition.position;
            Debug.Log("CHECK");
        }
        
        private void visualAid(bool value)
        {
            if (value)
            {
                foreach (Checkpoint cp in _nextCheckpoints)
                {
                    cp.GetComponent<Renderer>().material = materialNextCheckpoint;
                }
            }
            else
            {
                foreach (Checkpoint cp in _nextCheckpoints)
                {
                    cp.GetComponent<Renderer>().material = materialOtherCeckpoint;
                }
            }
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