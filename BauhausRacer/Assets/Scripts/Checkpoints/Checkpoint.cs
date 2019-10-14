using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer{
    public class Checkpoint : MonoBehaviour {
        private bool isNextCheckpoint;
        [SerializeField] private Transform _spawn;
        [SerializeField] public Checkpoint[] nextCheckpoints;
        public Transform SpawnPosition { get { return _spawn; } }
        public int CheckpointIndex;
        public ColorData _carColor { get; set; }

        private void Start(){
            _spawn = this.gameObject.transform.GetChild(0).transform;
            if(nextCheckpoints.Length == 0)
            {
                Debug.LogError("Missing checkpoint refference! You have to set a new checkpoint in " + gameObject.name);
            }
        }

        public void SetNextCheckpoint(bool value)
        {
            isNextCheckpoint = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player")
            {
                return;
            }

            if (isNextCheckpoint)
            {
                CheckpointManager.Instance.NextCheckpointArrived(this, nextCheckpoints);
                CheckpointManager.Instance.HideWrongDirection();
                _carColor = Game.Instance.ColorManager.CurrentColor;
            }
          
             else if (this.CheckpointIndex == CheckpointManager.Instance.CurrentCheckpointIndex)
            {
                if(CheckpointManager.Instance.WrongDirection){
                    CheckpointManager.Instance.HideWrongDirection();
                    foreach(Checkpoint cp in nextCheckpoints){
                        cp.isNextCheckpoint = true;
                    }
                } else {
                    CheckpointManager.Instance.ShowWrongDirection();
                }
            } else {
                 CheckpointManager.Instance.ShowWrongDirection();
            }

        }
    }
}
