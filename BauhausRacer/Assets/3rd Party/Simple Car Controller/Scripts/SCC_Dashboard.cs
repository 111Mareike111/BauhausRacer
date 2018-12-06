//----------------------------------------------
//            Simple Car Controller
//
// Copyright © 2017 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

// Class was used for handling UI dashboard. RPM and MPH gauges.
[AddComponentMenu("BoneCracker Games/Simple Car Controller/Dashboard")]
public class SCC_Dashboard : MonoBehaviour {

	public SCC_Drivetrain car;

	private float rpm = 0f;
	private float kmh = 0f;

	public RectTransform RPMNeedle;
	public RectTransform KMHNeedle;

	public float minimumRPMNeedleAngle = 0f;
	public float minimumKMHNeedleAngle = 0f;

	private float orgRPMNeedleAngle = 0f;
	private float orgKMHNeedleAngle = 0f;

	void Awake(){

		orgRPMNeedleAngle = RPMNeedle.transform.localEulerAngles.z;
		orgKMHNeedleAngle = KMHNeedle.transform.localEulerAngles.z;

	}

	void Update(){

		if (!car) {
			Debug.LogError ("Car is not selected on your SCC UI Canvas " + gameObject.name);
			enabled = false;
			return;
		}

		rpm = car.engineRPM * 1.2f;
		kmh = car.speed * 1.2f;

		Quaternion target = Quaternion.Euler (0f, 0f, orgKMHNeedleAngle - kmh);
		KMHNeedle.rotation = Quaternion.Slerp(KMHNeedle.rotation, target,  Time.deltaTime * 2f);

		Quaternion target2 = Quaternion.Euler (0f, 0f, orgRPMNeedleAngle - (rpm / 40f));
		RPMNeedle.rotation = Quaternion.Slerp(RPMNeedle.rotation, target2,  Time.deltaTime * 2f);
		
	}

}
