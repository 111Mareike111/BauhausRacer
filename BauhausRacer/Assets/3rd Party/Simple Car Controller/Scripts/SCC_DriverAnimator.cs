//----------------------------------------------
//            Simple Car Controller
//
// Copyright © 2017 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

// Class was used for feeding animator of the driver.
[AddComponentMenu("BoneCracker Games/Simple Car Controller/Driver Animator")]
public class SCC_DriverAnimator : MonoBehaviour {

	private SCC_Inputs inputs;

	public Animator driverAnimator;
	public string animatorParameter;

	void Start () {

		inputs = GetComponent<SCC_Inputs> ();
	
	}

	void Update () {

		if (!inputs) {
			enabled = false;
			return;
		}

		driverAnimator.SetFloat (animatorParameter, inputs.steering * 2f);
	
	}

}
