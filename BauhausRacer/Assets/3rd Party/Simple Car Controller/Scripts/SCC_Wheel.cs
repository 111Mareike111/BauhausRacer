//----------------------------------------------
//            Simple Car Controller
//
// Copyright © 2017 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// Script was used for handling wheels. Visual and physicaly. Based on Unity's WheelCollider.
[AddComponentMenu("BoneCracker Games/Simple Car Controller/Main Camera")]
[RequireComponent (typeof(WheelCollider))]
public class SCC_Wheel : MonoBehaviour {

	private Rigidbody rigid;
	private SCC_Drivetrain drivetrain;

	private WheelCollider _wheelCollider;
	public WheelCollider wheelCollider{
		get{
			if(_wheelCollider == null)
				_wheelCollider = GetComponent<WheelCollider>();
			return _wheelCollider;
		}set{
			_wheelCollider = value;
		}
	}
		
	public Transform wheelModel;

	private float wheelRotation = 0f;
	public float camber = 0f;

	internal bool isGrounded = false;
	internal float totalSlip = 0f;
	internal float rpm = 0f;
	internal float wheelRPMToSpeed = 0f;

	void Awake (){

		if(!wheelModel){
			Debug.LogError(transform.name + " wheel of the " + drivetrain.transform.name + " is missing wheel model. This wheel is disabled");
			enabled = false;
			return;
		}

		rigid = GetComponentInParent<Rigidbody>();
		drivetrain = GetComponentInParent<SCC_Drivetrain> ();
		wheelCollider = GetComponent<WheelCollider>();

		wheelCollider.mass = rigid.mass / 25f;

		GameObject fixedModel = new GameObject (wheelModel.name);
		fixedModel.transform.position = wheelModel.position;
		fixedModel.transform.SetParent (rigid.transform);
		fixedModel.transform.localRotation = Quaternion.identity;
		fixedModel.transform.localScale = Vector3.one;

		foreach (Transform go in wheelModel.GetComponentsInChildren<Transform>()) {

			go.SetParent (fixedModel.transform);
			
		}

		wheelModel = fixedModel.transform;

	}

	void Update(){

		if (!drivetrain || !wheelCollider) {
			enabled = false;
			return;
		}

		if (!drivetrain.enabled)
			return;

		WheelAlign();
		WheelCamber();

	}

	void FixedUpdate (){

		if (!drivetrain.enabled)
			return;

		WheelHit hit;
		isGrounded = wheelCollider.GetGroundHit(out hit);

		rpm = wheelCollider.rpm;
		wheelRPMToSpeed = (((wheelCollider.rpm * wheelCollider.radius) / 2.8f) * Mathf.Lerp(1f, .75f, hit.forwardSlip)) * rigid.transform.lossyScale.y;

	}

	public void WheelAlign (){

		if(!wheelModel){
			Debug.LogError(transform.name + " wheel of the " + drivetrain.transform.name + " is missing wheel model. This wheel is disabled");
			enabled = false;
			return;
		}

		RaycastHit hit;
		WheelHit CorrespondingGroundHit;

		Vector3 ColliderCenterPoint = wheelCollider.transform.TransformPoint(wheelCollider.center);
		wheelCollider.GetGroundHit(out CorrespondingGroundHit);

		if(Physics.Raycast(ColliderCenterPoint, -wheelCollider.transform.up, out hit, (wheelCollider.suspensionDistance + wheelCollider.radius) * transform.localScale.y) && !hit.collider.isTrigger && !hit.transform.IsChildOf(rigid.transform)){
			wheelModel.transform.position = hit.point + (wheelCollider.transform.up * wheelCollider.radius) * transform.localScale.y;
			float extension = (-wheelCollider.transform.InverseTransformPoint(CorrespondingGroundHit.point).y - wheelCollider.radius) / wheelCollider.suspensionDistance;
			Debug.DrawLine(CorrespondingGroundHit.point, CorrespondingGroundHit.point + wheelCollider.transform.up * (CorrespondingGroundHit.force / rigid.mass), extension <= 0.0 ? Color.magenta : Color.white);
			Debug.DrawLine(CorrespondingGroundHit.point, CorrespondingGroundHit.point - wheelCollider.transform.forward * CorrespondingGroundHit.forwardSlip * 2f, Color.green);
			Debug.DrawLine(CorrespondingGroundHit.point, CorrespondingGroundHit.point - wheelCollider.transform.right * CorrespondingGroundHit.sidewaysSlip * 2f, Color.red);
		}else{
			wheelModel.transform.position = Vector3.Lerp(wheelModel.transform.position, ColliderCenterPoint - (wheelCollider.transform.up * wheelCollider.suspensionDistance) * transform.localScale.y, Time.deltaTime * 10f);
		}

		wheelRotation += wheelCollider.rpm * 6 * Time.deltaTime;
		wheelModel.transform.rotation = wheelCollider.transform.rotation * Quaternion.Euler(wheelRotation, wheelCollider.steerAngle, wheelCollider.transform.rotation.z);

	}

	public void WheelCamber (){

		Vector3 wheelLocalEuler;

		if(wheelCollider.transform.localPosition.x < 0)
			wheelLocalEuler = new Vector3(wheelCollider.transform.localEulerAngles.x, wheelCollider.transform.localEulerAngles.y, (-camber));
		else
			wheelLocalEuler = new Vector3(wheelCollider.transform.localEulerAngles.x, wheelCollider.transform.localEulerAngles.y, (camber));

		Quaternion wheelCamber = Quaternion.Euler(wheelLocalEuler);
		wheelCollider.transform.localRotation = wheelCamber;

	}

}