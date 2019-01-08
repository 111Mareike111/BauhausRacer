using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer{
    public class Checkpoint : MonoBehaviour {
        private bool isNextCheckpoint;
        [SerializeField] private Transform _spawn;
        [SerializeField] private Checkpoint[] nextCheckpoints;
        public Transform SpawnPosition { get { return _spawn; } }

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

        private void OnTriggerExit(Collider other)
        {
            if (other.tag != "Player")
            {
                return;
            }

            if (isNextCheckpoint)
            {
                CheckpointManager.Instance.NextCheckpointArrived(this, nextCheckpoints);
                Debug.Log("nextCheckpoint");
            }
            else if (this == CheckpointManager.Instance.LastCheckpoint)
            {
                if(CheckpointManager.Instance.WrongDirection){
                    CheckpointManager.Instance.HideWrongDirection();
                    Debug.Log("to right");
                    foreach(Checkpoint cp in nextCheckpoints){
                        cp.isNextCheckpoint = true;
                    }
                } else {
                    CheckpointManager.Instance.ShowWrongDirection();
                Debug.Log("wrong");
                }

                
            }

        }
    }
}
