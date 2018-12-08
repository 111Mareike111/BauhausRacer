using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer {
	public class MyCarController : MonoBehaviour {
		public WheelCollider WheelFL;
		public WheelCollider WheelFR;
		public WheelCollider WheelRL;
		public WheelCollider WheelRR;
		public Transform WheelFLtrans;
		public Transform WheelFRtrans;
		public Transform WheelRLtrans;
		public Transform WheelRRtrans;
		
		private Vector3 eulertest;
	//	public float maxFwdSpeed = -3000;
		//public float maxBwdSpeed = 1000f;
	//	public float gravity = 9.8f;
		private bool braked = false;
		public float maxBrakeTorque = 500;
		public float maxSteerAngle = 45;
		private Rigidbody rb;
		public Transform centreofmass;
		public float maxTorque = 1000;
		void Start () 
		{
			rb = GetComponent<Rigidbody>();
			rb.centerOfMass = centreofmass.transform.localPosition;
			Time.timeScale = 2f;
			
		}
		
	void FixedUpdate () {
		if(!braked){
				WheelFL.brakeTorque = 0;
				WheelFR.brakeTorque = 0;
				WheelRL.brakeTorque = 0;
				WheelRR.brakeTorque = 0;
			}
			//speed of car, Car will move as you will provide the input to it.
	
			WheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical");
			WheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical");
		
			if(Input.GetAxis("Vertical")==0){
				WheelRR.motorTorque = 0;
				WheelRL.motorTorque = 0;
			}
			
			//changing car direction
			//changing the steer angle of the front wheels of the car so that we can change the car direction.
			WheelFL.steerAngle = maxSteerAngle * (Input.GetAxis("Horizontal"));
			WheelFR.steerAngle =  maxSteerAngle * Input.GetAxis("Horizontal");
		}
		void Update()
		{
			HandBrake();
			
			//for tyre rotate
			WheelFLtrans.Rotate(WheelFL.rpm/60*360*Time.deltaTime,0f ,0f);
			WheelFRtrans.Rotate(WheelFR.rpm/60*360*Time.deltaTime,0f ,0f);
			WheelRLtrans.Rotate(WheelRL.rpm/60*360*Time.deltaTime,0f ,0f);
			WheelRRtrans.Rotate(WheelRL.rpm/60*360*Time.deltaTime,0f ,0f);
			//changing tyre direction
			Vector3 temp = WheelFLtrans.localEulerAngles;
			Vector3 temp1 = WheelFRtrans.localEulerAngles;
			temp.y = WheelFL.steerAngle - (WheelFLtrans.localEulerAngles.z);
			WheelFLtrans.localEulerAngles = temp;
			temp1.y = WheelFR.steerAngle - WheelFRtrans.localEulerAngles.z;
			WheelFRtrans.localEulerAngles = temp1;
			eulertest = WheelFLtrans.localEulerAngles;
		}
		void HandBrake()
		{
			//Debug.Log("brakes " + braked);
			if(Input.GetButton("Jump"))
			{
				braked = true;
			}
			else
			{
				braked = false;
			}
			if(braked){
			
				WheelRL.brakeTorque = maxBrakeTorque * 20;//0000;
				WheelRR.brakeTorque = maxBrakeTorque * 20;//0000;
				WheelRL.motorTorque = 0;
				WheelRR.motorTorque = 0;
			}
		}
	}
}