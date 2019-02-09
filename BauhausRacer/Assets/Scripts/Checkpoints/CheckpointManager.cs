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

        private bool _wrongDirection;
        public int CurrentCheckpointIndex {get; set;}
        public GameObject turnBack_img;

        public Checkpoint[] NextCheckpoints { get { return _nextCheckpoints; } }
        public Checkpoint LastCheckpoint { get { return _currentCheckpoint; } }
        public bool WrongDirection {get { return _wrongDirection;}}
        
        public Checkpoint FirstCheckpoint { get { return _firstCheckpiont; } }
        private int checkpointIndex;

        [SerializeField] private UnityStandardAssets.Vehicles.Car.CarController _playerTranform;
        [SerializeField] private Material materialNextCheckpoint;
        [SerializeField] private Material materialOtherCeckpoint;
        [SerializeField] private Material materialLastCeckpoint;
        [SerializeField] private Material materialCurrentCeckpoint;
        private int _currentRound = -1;
        private Checkpoint[] checkpoints;

        public AudioSource checkpointSound;
        public AudioSource finishSound;
        public AudioSource resetSound;
        public AudioSource newRoundSound;

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
           _wrongDirection = false;
        }

        // Use this for initialization
        void Start () {
            _nextCheckpoints = new Checkpoint[1];
            _nextCheckpoints[0] = _firstCheckpiont;
            _firstCheckpiont.SetNextCheckpoint(true);
            _currentCheckpoint = _lastCheckpoint;
            visualAid(true);

        }

        public void NextCheckpointArrived(Checkpoint currentCheckpoint, Checkpoint[] nextCheckpoints)
        {
            _currentCheckpoint = currentCheckpoint;
           
            if(_currentCheckpoint == _firstCheckpiont)
            {
                _currentRound++;
                newRoundSound.Play();
                guiController.DisplayRounds(_currentRound);
                if(_currentRound == Game.Instance.rounds){
                    guiController.ShowHighscorePanel();
                    finishSound.Play();

                }
                //Todo Increment course rounds
            } else {
                 checkpointSound.Play();
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
            Debug.Log("Round: "+_currentRound);
            CurrentCheckpointIndex = currentCheckpoint.CheckpointIndex;
        }

        public void ResetPlayerToCurrentCheckpoint()
        {
            resetSound.Play();
            _playerTranform.transform.position = _currentCheckpoint.SpawnPosition.position;
            _playerTranform.transform.rotation = _currentCheckpoint.SpawnPosition.rotation;
            _playerTranform.transform.LookAt(_currentCheckpoint.transform.GetChild(1));
            _playerTranform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            HideWrongDirection();
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
            _currentCheckpoint.GetComponent<Renderer>().material = materialCurrentCeckpoint;
        }

        public void ShowWrongDirection()
        {
                _wrongDirection = true;
                turnBack_img.SetActive(true);
                Debug.Log("wrong direction");
        }

        public void HideWrongDirection(){
            _wrongDirection = false;
            turnBack_img.SetActive(false);
        }

    }
}