//----------------------------------------------
//            Simple Car Controller
//
// Copyright © 2017 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

// Class was used for receiving player's inputs, and feeding car's drivetrain. Currently they are using Unity's InputManager. Vertical and Horizontal Inputs.
[AddComponentMenu("BoneCracker Games/Simple Car Controller/Inputs")]
public class SCC_Inputs : MonoBehaviour {

	private SCC_Drivetrain drivetrain;

	internal float gas;
	internal float brake;
	internal float steering;
	internal float handbrake;

	void Start(){

		drivetrain = GetComponent<SCC_Drivetrain> ();

	}

	void Update(){

		if (!drivetrain) {
			enabled = false;
			return;
		}

		ReceiveInputs ();
		FeedDrivetrain ();

	}

	void ReceiveInputs () {

		gas = Mathf.Clamp01(Input.GetAxis ("Vertical"));
		brake = Mathf.Abs(Mathf.Clamp(Input.GetAxis ("Vertical"), -1f, 0f));
		steering = Input.GetAxis ("Horizontal");
		handbrake = Input.GetKey (KeyCode.Space) ? 1f : 0f;
	 
	}

	void FeedDrivetrain(){

		drivetrain.inputGas = gas;
		drivetrain.inputBrake = brake;
		drivetrain.inputSteering = steering;
		drivetrain.inputHandbrake = handbrake;

	}

}
