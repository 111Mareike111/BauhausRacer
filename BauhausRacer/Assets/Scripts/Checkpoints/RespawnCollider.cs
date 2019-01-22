using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer{
public class RespawnCollider : MonoBehaviour {

	public void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			CheckpointManager.Instance.ResetPlayerToCurrentCheckpoint();
		}
	}
}
}
