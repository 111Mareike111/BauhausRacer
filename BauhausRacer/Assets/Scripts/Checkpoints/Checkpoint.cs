using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer{
    public class Checkpoint : MonoBehaviour {
        [SerializeField] private Transform _spawn;
        public Transform SpawnPosition { get { return _spawn; } }
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.tag != "Player")
            {
                return;
            } 
            if(this == CheckpointManager.Instance.NextCheckpoint)
            {
                CheckpointManager.Instance.NextCheckpointArrived();
            }
            else if(this == CheckpointManager.Instance.LastCheckpoint)
            {
                CheckpointManager.Instance.WrongDirection();
            }
            
        }
    }
}
