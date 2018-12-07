//----------------------------------------------
//            Simple Car Controller
//
// Copyright © 2017 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

// Script was used for stabilizing the car to avoid flip overs.
[AddComponentMenu("BoneCracker Games/Simple Car Controller/Rigid Stabilizer")]
public class SCC_RigidStabilizer : MonoBehaviour {

	private Rigidbody rigid;
	private SCC_Wheel[] wheels;

	public float reflection = 100f;
	public float stability = .5f;

	void Start () {

		rigid = GetComponent<Rigidbody> ();
		wheels = GetComponentsInChildren<SCC_Wheel> ();

	}

	void FixedUpdate () {

		if (!rigid) {
			enabled = false;
			return;
		}

		Vector3 predictedUp = Quaternion.AngleAxis(rigid.velocity.magnitude * Mathf.Rad2Deg * stability / reflection, rigid.angularVelocity) * transform.up;
		Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);

		bool grounded = false;

		for (int i = 0; i < wheels.Length; i++) {

			if (wheels [i].isGrounded)
				grounded = true;

		}
		 
		if(!grounded)
			rigid.AddTorque(torqueVector * reflection * reflection);
	
	}

}
