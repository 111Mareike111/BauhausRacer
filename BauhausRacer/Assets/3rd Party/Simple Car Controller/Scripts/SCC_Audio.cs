//----------------------------------------------
//            Simple Car Controller
//
// Copyright © 2017 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

// This class handles engine audio based on vehicle engine rpm.
[AddComponentMenu("BoneCracker Games/Simple Car Controller/Audio")]
public class SCC_Audio : MonoBehaviour {

	private SCC_Drivetrain drivetrain;
	private SCC_Inputs inputs;

	public AudioClip engineOn;
	public AudioClip engineOff;

	private AudioSource engineOnSource;
	private AudioSource engineOffSource;

	public float minimumVolume = .1f;
	public float maximumVolume = 1f;

	public float minimumPitch = .75f;
	public float maximumPitch = 1.25f;

	void Start () {

		drivetrain = GetComponent<SCC_Drivetrain> ();
		inputs = GetComponent<SCC_Inputs> ();

		GameObject engineOnGO = new GameObject ("Engine On AudioSource");
		engineOnGO.transform.SetParent (transform, false);
		engineOnSource = engineOnGO.AddComponent<AudioSource> ();
		engineOnSource.clip = engineOn;
		engineOnSource.loop = true;
		engineOnSource.spatialBlend = 1f;
		engineOnSource.Play ();

		GameObject engineOffGO = new GameObject ("Engine Off AudioSource");
		engineOffGO.transform.SetParent (transform, false);
		engineOffSource = engineOffGO.AddComponent<AudioSource> ();
		engineOffSource.clip = engineOff;
		engineOffSource.loop = true;
		engineOffSource.spatialBlend = 1f;
		engineOffSource.Play ();
	
	}

	void Update () {

		if (!drivetrain || !inputs) {
			enabled = false;
			return;
		}
	
		engineOnSource.volume = Mathf.Lerp(minimumVolume, maximumVolume, inputs.gas);
		engineOffSource.volume = Mathf.Lerp(maximumVolume, 0f, inputs.gas);

		engineOnSource.pitch = Mathf.Lerp(minimumPitch, maximumPitch, drivetrain.engineRPM / drivetrain.maximumEngineRPM);
		engineOffSource.pitch = engineOnSource.pitch;

	}

}
