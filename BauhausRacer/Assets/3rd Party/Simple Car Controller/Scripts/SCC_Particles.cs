//----------------------------------------------
//            Simple Car Controller
//
// Copyright © 2017 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Class was used for handling particle systems on car. Exhausts and wheel particles.
[AddComponentMenu("BoneCracker Games/Simple Car Controller/Particles")]
public class SCC_Particles : MonoBehaviour {

	private SCC_Inputs inputs;
	private SCC_Wheel[] wheels;

	public ParticleSystem[] exhaustParticles;
	private ParticleSystem.EmissionModule[] exhaustEmissions;

	public ParticleSystem wheelParticlePrefab;
	private List<ParticleSystem> createdParticles = new List<ParticleSystem>();
	private ParticleSystem.EmissionModule[] wheelEmissions;

	public float slip = .25f;

	void Start () {

		inputs = GetComponent<SCC_Inputs> ();
		wheels = GetComponentsInChildren<SCC_Wheel> ();

		if (!inputs) {
			Debug.LogError ("SCC_Inputs couldn't found on this gameobject!");
			enabled = false;
			return;
		}

		if (wheelParticlePrefab) {

			for (int i = 0; i < wheels.Length; i++) {

				ParticleSystem particle = (ParticleSystem)Instantiate (wheelParticlePrefab, wheels [i].transform.position, wheels [i].transform.rotation, wheels [i].transform);
				createdParticles.Add (particle.GetComponent<ParticleSystem> ());

			}

			wheelEmissions = new ParticleSystem.EmissionModule[createdParticles.Count];

			for (int i = 0; i < createdParticles.Count; i++) {
				wheelEmissions[i] = createdParticles[i].emission;
			}

		}

		if (exhaustParticles != null && exhaustParticles.Length >= 1) {

			exhaustEmissions = new ParticleSystem.EmissionModule[exhaustParticles.Length];

			for (int i = 0; i < exhaustParticles.Length; i++) {
				exhaustEmissions [i] = exhaustParticles [i].emission;
			}

		}

	}

	void Update(){

		WheelParticles ();
		ExhaustParticles ();

	}

	void WheelParticles () {

		if (!wheelParticlePrefab)
			return;
		 
		for (int i = 0; i < wheels.Length; i++) {

			WheelHit hit;
			wheels [i].wheelCollider.GetGroundHit (out hit);

			if (Mathf.Abs (hit.sidewaysSlip) >= slip || Mathf.Abs (hit.forwardSlip) >= slip) {
				wheelEmissions[i].enabled = true;
			} else {
				wheelEmissions[i].enabled = false;
			}

		}
	
	}

	void ExhaustParticles(){

		if (exhaustParticles.Length <= 0)
			return;

		for (int i = 0; i < exhaustEmissions.Length; i++) {

			exhaustEmissions [i].rate = Mathf.Lerp (1f, 20f, inputs.gas);

		}

	}

}
