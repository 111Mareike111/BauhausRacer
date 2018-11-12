using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    public static CheckpointManager Instance;

    private Checkpoint _currentCheckpoint;
    private Checkpoint _nextCheckpoint;
    public Checkpoint NextCheckpoint { get { return _nextCheckpoint; } }
    private int checkpointIndex;
    private int lab;
    [SerializeField] private Transform _playerTranform;
    [SerializeField] private Material materialNextCheckpoint;
    [SerializeField] private Material materialOtherCeckpoint;
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
    }

    // Use this for initialization
    void Start () {
		if(_playerTranform == null)
        {
            Debug.LogError("Missing Refernece: In the CheckpointManager is no Car");
        }
        checkpointIndex = 0;
        lab = 0;
        checkpoints = gameObject.GetComponentsInChildren<Checkpoint>();
        _currentCheckpoint = checkpoints[checkpointIndex];
        _nextCheckpoint = checkpoints[checkpointIndex+1];
        visualAid();

    }

    public void NextCheckpointArrived()
    {
        checkpointIndex++;
        if (checkpointIndex == checkpoints.Length-1)
        {
            _currentCheckpoint = checkpoints[checkpointIndex];
            _nextCheckpoint = checkpoints[0];
        }
        else if (checkpointIndex == checkpoints.Length)
        {
            checkpointIndex = 0;
            _currentCheckpoint = checkpoints[checkpointIndex];
            _nextCheckpoint = checkpoints[checkpointIndex + 1];
            lab++;
        }
        else
        {
            _currentCheckpoint = checkpoints[checkpointIndex];
            _nextCheckpoint = checkpoints[checkpointIndex + 1];
        }
        visualAid();
    }

    public void ResetPlayerToCurrentCheckpoint()
    {
        _playerTranform = _currentCheckpoint.SpawnPosition;
    }
	
    private void visualAid()
    {
        _nextCheckpoint.GetComponent<Renderer>().material = materialNextCheckpoint;
        _currentCheckpoint.GetComponent<Renderer>().material = materialOtherCeckpoint;
    }


}
