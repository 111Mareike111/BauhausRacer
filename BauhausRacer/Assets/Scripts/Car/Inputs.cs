//----------------------------------------------
//            Simple Car Controller
//
// Copyright © 2017 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BauhausRacer{

// Class was used for receiving player's inputs, and feeding car's drivetrain. Currently they are using Unity's InputManager. Vertical and Horizontal Inputs.
[AddComponentMenu("BoneCracker Games/Simple Car Controller/Inputs")]
public class Inputs : MonoBehaviour {

	private Driving drivetrain;

	internal float gas;
	internal float brake;
	internal float steering;
	internal float handbrake;

	void Start(){

		drivetrain = GetComponent<Driving> ();

	}

	void Update(){

		if (!drivetrain) {
			enabled = false;
			return;
		}

		if(!Game.Instance.gameStopped){ 	//stop game without setting timescale to 0
			ReceiveInputs ();
			FeedDrivetrain ();
		}

	}

	void ReceiveInputs () {	
		if(Input.GetJoystickNames().Length == 0) {
			gas = Mathf.Clamp01(Input.GetAxis ("Vertical"));
			brake = Mathf.Abs(Mathf.Clamp(Input.GetAxis ("Vertical"), -1f, 0f));
			steering = Input.GetAxis ("Horizontal");
			handbrake = Input.GetKey (KeyCode.Space) ? 1f : 0f;
		} else {
			gas = Mathf.Clamp01(Input.GetAxis ("Vertical2"));
			brake = Mathf.Abs(Mathf.Clamp(Input.GetAxis ("Vertical2"), -1f, 0f));
			steering = Input.GetAxis ("Horizontal2");
			handbrake = Input.GetKey (KeyCode.Space) ? 1f : 0f;
		}
	}

	void FeedDrivetrain(){

		drivetrain.inputGas = gas;
		drivetrain.inputBrake = brake;
		drivetrain.inputSteering = steering;
		drivetrain.inputHandbrake = handbrake;

	}

}
}