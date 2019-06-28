using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CameraFieldOfView : MonoBehaviour {
    [SerializeField] private Camera camera;
    [SerializeField] private CarController carController;
    public float foVMin = 60;
    public float foVMax = 90;
    private float intensity; 
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (carController.CurrentSpeed == 0 || carController.MaxSpeed == 0)
        {
            intensity = 0;
        }
        else
        {
            intensity = Mathf.Clamp(carController.CurrentSpeed / carController.MaxSpeed, 0, 1);
        }
        camera.fieldOfView = (foVMax - foVMin) * intensity + foVMin;
    }
}
