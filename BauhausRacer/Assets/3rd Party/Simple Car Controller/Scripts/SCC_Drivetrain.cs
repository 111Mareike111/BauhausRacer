//----------------------------------------------
//            Simple Car Controller
//
// Copyright © 2017 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

// Class was used for handling drivetrain, which is most part of the car controller. 
[AddComponentMenu("BoneCracker Games/Simple Car Controller/Drivetrain")]
[RequireComponent (typeof(Rigidbody))]
public class SCC_Drivetrain : MonoBehaviour {

	private Rigidbody rigid;

	public SCC_Wheels[] wheels;

	[System.Serializable]
	public class SCC_Wheels{

		public Transform wheelTransform;
		public SCC_Wheel wheelCollider;

		public bool isSteering = false;
		[Range(-45f, 45f)]public float steeringAngle = 25f;
		public bool isTraction = false;
		public bool isBrake = false;
		public bool isHandbrake = false;

	}

	internal float processedGas{get{

			return (direction == 1 ? Mathf.Clamp01(inputGas) : Mathf.Clamp01(inputBrake));

		}set{inputGas = value;}}

	internal float processedBrake{get{

			return (direction == 1 ? Mathf.Clamp01(inputBrake) : Mathf.Clamp01(inputGas));

		}set{inputBrake = value;}}

	public float inputGas;
	public float inputBrake;
	public float inputSteering;
	public float inputHandbrake;

	public Transform COM;
	public float speed = 0f;

	public float engineRPM = 0f;
	public float minimumEngineRPM = 650f;
	public float maximumEngineRPM = 7000f;

	public float engineTorque = 1000f;
	public float brakeTorque = 1000f;
	public float maximumSpeed = 100f;

	public int direction = 1;
	public float finalDriveRatio = 3.2f;

	public float highSpeedSteerAngle = 100f;

	private float timerForReverse = 0f;

	void Start () {

		rigid = GetComponent<Rigidbody> ();

		rigid.maxAngularVelocity = 6f;
		rigid.centerOfMass = COM.localPosition;

	}

	void FixedUpdate () {

		Engine ();
		ApplySteering ();
		ApplyTraction ();
		ApplyBrake ();
		Others ();

	}

	void Engine(){
		
		speed = rigid.velocity.magnitude * 3.6f;
		engineRPM = Mathf.Lerp(minimumEngineRPM, maximumEngineRPM, ((wheels [0].wheelCollider.wheelRPMToSpeed + wheels [1].wheelCollider.wheelRPMToSpeed) / 2f) / maximumSpeed);

	}

	void ApplySteering(){

		for (int i = 0; i < wheels.Length; i++) {

			if (wheels [i].isSteering) {
				wheels [i].wheelCollider.wheelCollider.steerAngle = (wheels [i].steeringAngle * inputSteering) * Mathf.Lerp(1f, .25f, speed / highSpeedSteerAngle);
			}
		}

	}

	void ApplyTraction(){

		int totalTractionWheels = 0;

		for (int i = 0; i < wheels.Length; i++) {

			if (wheels [i].isTraction)
				totalTractionWheels++;

		}

		for (int i = 0; i < wheels.Length; i++) {
			
			if (wheels [i].isTraction)
				wheels [i].wheelCollider.wheelCollider.motorTorque = ((engineTorque * finalDriveRatio) * processedGas * direction) / totalTractionWheels;

			if ((speed >= maximumSpeed || wheels[i].wheelCollider.wheelRPMToSpeed >= maximumSpeed) && wheels [i].isTraction)
				wheels [i].wheelCollider.wheelCollider.motorTorque = 0f;

		}

	}

	void ApplyBrake(){

		int totalBrakeWheels = 0;

		for (int i = 0; i < wheels.Length; i++) {

			if (wheels [i].isBrake)
				totalBrakeWheels++;

		}

		for (int i = 0; i < wheels.Length; i++) {

			if (wheels [i].isBrake && wheels [i].isHandbrake) {
				
				wheels [i].wheelCollider.wheelCollider.brakeTorque = ((brakeTorque * processedBrake) + ((brakeTorque * 5f) * inputHandbrake)) / totalBrakeWheels;

			} else {

				if (wheels [i].isBrake)
					wheels [i].wheelCollider.wheelCollider.brakeTorque = (brakeTorque * processedBrake) / totalBrakeWheels;

				if (wheels [i].isHandbrake)
					wheels [i].wheelCollider.wheelCollider.brakeTorque = ((wheels [i].wheelCollider.wheelCollider.brakeTorque + (brakeTorque * 5f)) * inputHandbrake) / totalBrakeWheels;

			}

		}

	}

	void Others(){

		if (speed <= 5f && inputBrake >= .75f)
			timerForReverse += Time.fixedDeltaTime;
		else if(inputBrake <= .25f)
			timerForReverse = 0f;

		if (timerForReverse >= 1)
			direction = -1;
		else
			direction = 1;

	}

}
