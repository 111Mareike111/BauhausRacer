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
        public GameObject turnBack_img;

        public Checkpoint[] NextCheckpoints { get { return _nextCheckpoints; } }
        public Checkpoint LastCheckpoint { get { return _currentCheckpoint; } }
        public bool WrongDirection {get { return _wrongDirection;}}
        
        public Checkpoint FirstCheckpoint { get { return _firstCheckpiont; } }
        private int checkpointIndex;

        [SerializeField] private Transform _playerTranform;
        [SerializeField] private Material materialNextCheckpoint;
        [SerializeField] private Material materialOtherCeckpoint;
        [SerializeField] private Material materialLastCeckpoint;
        [SerializeField] private Material materialCurrentCeckpoint;
        private int _currentRound =0;
        private Checkpoint[] checkpoints;

        public AudioSource checkpointSound;
        public AudioSource finishSound;
        public AudioSource resetSound;

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
            checkpointSound.Play();
            if(_currentCheckpoint == _lastCheckpoint)
            {
                _currentRound++;
                guiController.DisplayRounds(_currentRound);
                if(_currentRound == Game.Instance.rounds){
                    guiController.ShowHighscorePanel();
                    finishSound.Play();

                }
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
            Debug.Log("Round: "+_currentRound);
        }

        public void ResetPlayerToCurrentCheckpoint()
        {
            resetSound.Play();
            _playerTranform.position = _currentCheckpoint.SpawnPosition.position;
            _playerTranform.rotation = _currentCheckpoint.SpawnPosition.rotation;
            _playerTranform.LookAt(_currentCheckpoint.transform.GetChild(1));
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