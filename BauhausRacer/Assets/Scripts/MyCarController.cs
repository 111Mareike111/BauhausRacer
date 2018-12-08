using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer {
	public class MyCarController : MonoBehaviour {

		public float speed = 90f;
		public float turnSpeed = 5f;
		//public float hoverForce = 65f;
		//public float hoverHeight = 3.5f;
		private float powerInput;
		private float turnInput;
		private Rigidbody carRigidbody;


    void Awake () 
    {
        carRigidbody = GetComponent <Rigidbody>();
    }

    void Update () 
    {
        powerInput = Input.GetAxis ("Vertical");
        turnInput = Input.GetAxis ("Horizontal");
    }

    void FixedUpdate()
    {
        Ray ray = new Ray (transform.position, -transform.up);
        RaycastHit hit;

     /*/   if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            carRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
	}*/

        carRigidbody.AddRelativeForce(0f, 0f, powerInput * speed);
        carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);

    }

	/*/	public float deadZone = .001f;
		public float horizontal;
		public float maxSpeedToTurn = 0.25f;

		public Transform frontLeftWheel;
		public Transform frontRightWheel;
		public Transform rearLeftWheel;
		public Transform rearRightWheel;

		public Transform LFWheelTransform;
		public Transform RFWheelTransform;

		public float mySpeed;

		public float power = 1200f;
		public float maxSpeed = 50f;
		public float carGrip = 70f;
		public float turnSpeed = 2.5f;
		private Rigidbody carRigidbody;

		private float slideSpeed;
		private Vector3 carRight;
		private Vector3 carFwd;
		private Vector3 tempVEC;
		private Vector3 rotationAmount;
		private Vector3 accel;
		public float throttle;
		private Vector3 myRight;
		private Vector3 velo;
		private Vector3 flatVelo;
		private Vector3 relativeVelocity;
		private Vector3 dir;
		private Vector3 flatDir;
		private Vector3 carUp;
		private Transform carTransform;
		private Vector3 engineForce;
		private float actualGrip;
		private Vector3 turnVec;
		private Vector3 imp;
		private float rev;
		private float actualturn;
		private float carMass;
		private Transform[] wheelTransform = new Transform[4];

		void Start(){
			InitializeCar();
		}

		void InitializeCar(){
			carTransform = GetComponent<Transform>();
			carRigidbody = GetComponent<Rigidbody>();
			carUp = carTransform.up;
			carMass = carRigidbody.mass;
			carFwd = Vector3.forward;
			carRight = Vector3.right;
			SetUpWheels();
			carRigidbody.centerOfMass= new Vector3 (0f, -0.75f, .35f);
		}
		
		void Update(){
			CarPhysicsUpdate();
			
			horizontal = Input.GetAxis("Horizontal");
			throttle = Input.GetAxis("Vertical");
		}

		void LateUpdate(){
			RotateVisualWheels();

		}

		void SetUpWheels(){
			if((null == frontLeftWheel) || (null == frontRightWheel) || (null == rearLeftWheel) || (null == rearRightWheel)){
				Debug.LogError("one or more wheels are not assigned");
			} else{
				wheelTransform[0] = frontLeftWheel;
				wheelTransform[1] = rearLeftWheel;
				wheelTransform[2] = frontRightWheel;
				wheelTransform[3] = rearRightWheel;
			}
		}

		void RotateVisualWheels(){
			Vector3 tmpEuelerAngles = LFWheelTransform.localEulerAngles;
			tmpEuelerAngles.y = horizontal * 30f;

			LFWheelTransform.localEulerAngles = tmpEuelerAngles;
			RFWheelTransform.localEulerAngles = tmpEuelerAngles;

			rotationAmount = carRight * (relativeVelocity.z * 1.6f * Time.deltaTime * Mathf.Rad2Deg);
			wheelTransform[0].Rotate(rotationAmount);
			wheelTransform[1].Rotate(rotationAmount);
			wheelTransform[2].Rotate(rotationAmount);
			wheelTransform[3].Rotate(rotationAmount);
		}

		void CarPhysicsUpdate(){
			myRight = carTransform.right;
			velo = carRigidbody.velocity;
			tempVEC = new Vector3(velo.x, 0f, velo.z);
			flatVelo = tempVEC;
			dir = transform.TransformDirection(carFwd);
			flatDir = Vector3.Normalize(tempVEC);
			relativeVelocity = carTransform.InverseTransformDirection(flatVelo);
			slideSpeed = Vector3.Dot(myRight, flatVelo);
			mySpeed = flatVelo.magnitude;
			rev = Mathf.Sign(Vector3.Dot(flatVelo, flatDir));

			engineForce = (flatDir* (power * throttle)* carMass);
			actualturn = horizontal;

			if(rev <0.1f){
				actualturn = -actualturn;
			}

			turnVec = (((carUp*turnSpeed)*actualturn)*carMass)*800f;
			actualGrip = Mathf.Lerp(100f, carGrip, mySpeed * 0.02f);
			imp = myRight *(-slideSpeed * carMass * actualGrip);
		}
		void FixedUpdate(){
			if(mySpeed < maxSpeed) {
				carRigidbody.AddForce(engineForce * Time.deltaTime);
			
			}
			if(mySpeed > maxSpeedToTurn){
				carRigidbody.AddTorque(turnVec * Time.deltaTime);
			} else if (mySpeed <maxSpeedToTurn){
				return;
			}
			carRigidbody.AddForce(imp *Time.deltaTime);
		}*/
	
	
	/*	public WheelCollider WheelFL;
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
		}*/
	}
}