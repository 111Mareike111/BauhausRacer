using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    [SerializeField] private Transform _spawn;
    public Transform SpawnPosition { get { return _spawn; } }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag != "Player" || this != CheckpointManager.Instance.NextCheckpoint)
        {
            return;
        }
        CheckpointManager.Instance.NextCheckpointArrived();
    }




}
