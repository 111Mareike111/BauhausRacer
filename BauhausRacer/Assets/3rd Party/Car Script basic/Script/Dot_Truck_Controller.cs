using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Dot_Truck : System.Object
{
	public WheelCollider leftWheel;
	public GameObject leftWheelMesh;
	public WheelCollider rightWheel;
	public GameObject rightWheelMesh;
	public bool motor;
	public bool steering;
	public bool reverseTurn; 
}

public class Dot_Truck_Controller : MonoBehaviour {

	public float maxMotorTorque;
	public float maxSteeringAngle;
	public List<Dot_Truck> truck_Infos;

	private Rigidbody rigidbody;

	WheelCollider WheelL; 
	WheelCollider WheelR;
	float AntiRoll = 10000.0f; 

	void Awake(){
		rigidbody = GetComponent<Rigidbody>();
	}
	
	public void FixedUpdate () { 
		WheelHit hit; 
		float travelL = 1.0f;
		float travelR = 1.0f; 
		var groundedL = WheelL.GetGroundHit(out hit); 
		if (groundedL) travelL = (
			-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) /
			 WheelL.suspensionDistance; var groundedR = WheelR.GetGroundHit(out hit);
		if (groundedR) travelR = (
			-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / 
			WheelR.suspensionDistance; 
		float antiRollForce = (travelL - travelR) * AntiRoll; 
		if (groundedL) rigidbody.AddForceAtPosition(WheelL.transform.up * -antiRollForce, WheelL.transform.position); 
		if (groundedR) rigidbody.AddForceAtPosition(WheelR.transform.up * antiRollForce, WheelR.transform.position); }

	public void VisualizeWheel(Dot_Truck wheelPair)
	{
		Quaternion rot;
		Vector3 pos;
		wheelPair.leftWheel.GetWorldPose ( out pos, out rot);
		wheelPair.leftWheelMesh.transform.position = pos;
		wheelPair.leftWheelMesh.transform.rotation = rot;
		wheelPair.rightWheel.GetWorldPose ( out pos, out rot);
		wheelPair.rightWheelMesh.transform.position = pos;
		wheelPair.rightWheelMesh.transform.rotation = rot;
	}

	public void Update()
	{
		float motor = maxMotorTorque * Input.GetAxis("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
		float brakeTorque = Mathf.Abs(Input.GetAxis("Jump"));
		if (brakeTorque > 0.001) {
			brakeTorque = maxMotorTorque;
			motor = 0;
		} else {
			brakeTorque = 0;
		}

		foreach (Dot_Truck truck_Info in truck_Infos)
		{
			if (truck_Info.steering == true) {
				truck_Info.leftWheel.steerAngle = truck_Info.rightWheel.steerAngle = ((truck_Info.reverseTurn)?-1:1)*steering;
			}

			if (truck_Info.motor == true)
			{
				truck_Info.leftWheel.motorTorque = motor;
				truck_Info.rightWheel.motorTorque = motor;
			}

			truck_Info.leftWheel.brakeTorque = brakeTorque;
			truck_Info.rightWheel.brakeTorque = brakeTorque;

			VisualizeWheel(truck_Info);
		}

	}

	


}