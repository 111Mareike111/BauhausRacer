using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CarCameraEffects : MonoBehaviour {
    [SerializeField] private CarController carController;
    [SerializeField] private CameraFieldOfView cameraFieldOfView;
    [SerializeField] private Speedlines speedlines;
    private float intensity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetInensity();
        cameraFieldOfView.ChangeFieldOfView(intensity);
        speedlines.ChangeSpeedlinesByIntensity(intensity);
    }

    private void GetInensity()
    {
        if (carController.MaxSpeed == 0)
        {
            intensity = 0;
        }
        else
        {
            intensity = Mathf.Clamp(carController.CurrentSpeed / carController.MaxSpeed, 0, 1);
        }
        if(intensity < 0.01)
        {
            intensity = 0;
        }
    }
}
