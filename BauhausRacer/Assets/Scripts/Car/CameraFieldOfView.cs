using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CameraFieldOfView : MonoBehaviour {
    [SerializeField] private Camera camera;
    public float foVMin = 60;
    public float foVMax = 90;
    private float intensity; 
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame

    public void ChangeFieldOfView(float intensity)
    {
        camera.fieldOfView = (foVMax - foVMin) * intensity + foVMin;
    }
}
