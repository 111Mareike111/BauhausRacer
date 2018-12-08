//----------------------------------------------
//            Simple Car Controller
//
// Copyright © 2017 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

// Class was used for anti roll bars.
[AddComponentMenu("BoneCracker Games/Simple Car Controller/Antiroll")]
public class SCC_AntiRoll : MonoBehaviour {

	private Rigidbody rigid;

	public SCC_Wheel frontLeft;
	public SCC_Wheel frontRight;

	public SCC_Wheel rearLeft;
	public SCC_Wheel rearRight;

	public float antiRollHorizontal = 5000f;

	void Start(){

		rigid = GetComponent<Rigidbody> ();

		if (!frontLeft && !frontRight && !rearLeft && !rearRight)
			enabled = false;

	}

	void FixedUpdate(){

		#region Horizontal

		if(frontLeft && frontRight){

		WheelHit FrontWheelHit;

		float travelFL = 1.0f;
		float travelFR = 1.0f;

		bool groundedFL= frontLeft.wheelCollider.GetGroundHit(out FrontWheelHit);

		if (groundedFL)
			travelFL = (-frontLeft.transform.InverseTransformPoint(FrontWheelHit.point).y - frontLeft.wheelCollider.radius) / frontLeft.wheelCollider.suspensionDistance;

		bool groundedFR= frontRight.wheelCollider.GetGroundHit(out FrontWheelHit);

		if (groundedFR)
			travelFR = (-frontRight.transform.InverseTransformPoint(FrontWheelHit.point).y - frontRight.wheelCollider.radius) / frontRight.wheelCollider.suspensionDistance;

		float antiRollForceFrontHorizontal= (travelFL - travelFR) * antiRollHorizontal;

		if (groundedFL)
			rigid.AddForceAtPosition(frontLeft.transform.up * -antiRollForceFrontHorizontal, frontLeft.transform.position); 
		if (groundedFR)
			rigid.AddForceAtPosition(frontRight.transform.up * antiRollForceFrontHorizontal, frontRight.transform.position);

		}

		if(rearLeft && rearRight){

		WheelHit RearWheelHit;

		float travelRL = 1.0f;
		float travelRR = 1.0f;

		bool groundedRL= rearLeft.wheelCollider.GetGroundHit(out RearWheelHit);

		if (groundedRL)
			travelRL = (-rearLeft.transform.InverseTransformPoint(RearWheelHit.point).y - rearLeft.wheelCollider.radius) / rearLeft.wheelCollider.suspensionDistance;

		bool groundedRR= rearRight.wheelCollider.GetGroundHit(out RearWheelHit);

		if (groundedRR)
			travelRR = (-rearRight.transform.InverseTransformPoint(RearWheelHit.point).y - rearRight.wheelCollider.radius) / rearRight.wheelCollider.suspensionDistance;

		float antiRollForceRearHorizontal= (travelRL - travelRR) * antiRollHorizontal;

		if (groundedRL)
			rigid.AddForceAtPosition(rearLeft.transform.up * -antiRollForceRearHorizontal, rearLeft.transform.position); 
		if (groundedRR)
			rigid.AddForceAtPosition(rearRight.transform.up * antiRollForceRearHorizontal, rearRight.transform.position);

		}

		#endregion

	}

}
