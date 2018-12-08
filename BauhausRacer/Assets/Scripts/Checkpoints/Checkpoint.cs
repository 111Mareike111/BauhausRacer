using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer{
    public class Checkpoint : MonoBehaviour {
        [SerializeField] private Transform _spawn;
        public Transform SpawnPosition { get { return _spawn; } }

        private void Start(){
            _spawn = transform;
        }
        private void OnTriggerExit(Collider other)
        {
            
            if (other.tag != "Player")
            {
                return;
            } 
            Debug.Log(other);
                if(this == CheckpointManager.Instance.NextCheckpoint)
                {
                    Debug.Log("next");
                    CheckpointManager.Instance.NextCheckpointArrived();
                }
                else if(this == CheckpointManager.Instance.LastCheckpoint)
                {
                
                    CheckpointManager.Instance.WrongDirection();
                }
            
           
        }
    }
}
