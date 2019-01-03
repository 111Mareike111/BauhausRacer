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
            _spawn = transform;
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
            }
            else if (this == CheckpointManager.Instance.LastCheckpoint)
            {

                CheckpointManager.Instance.WrongDirection();
            }

        }
    }
}
