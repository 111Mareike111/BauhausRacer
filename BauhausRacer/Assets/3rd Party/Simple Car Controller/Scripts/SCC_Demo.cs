using UnityEngine;
using System.Collections;

public class SCC_Demo : MonoBehaviour {

	public GameObject[] spawnableCars;
	public Transform defaultSpawnPoint;

	public bool destroyAllCars = true;

	public void SpawnCar (int selectedCar) {

		Vector3 spawnPoint = new Vector3();
		Quaternion spawnRot = new Quaternion();

		if (destroyAllCars) {

			SCC_Drivetrain[] cars = GameObject.FindObjectsOfType<SCC_Drivetrain> ();

			foreach (SCC_Drivetrain car in cars) {
				Destroy (car.gameObject);
			}

		}

		if (GameObject.FindObjectOfType<SCC_Camera> ()) {
			spawnPoint = GameObject.FindObjectOfType<SCC_Camera> ().transform.position;
			spawnRot = GameObject.FindObjectOfType<SCC_Camera> ().transform.rotation;
			spawnPoint += GameObject.FindObjectOfType<SCC_Camera> ().transform.forward * GameObject.FindObjectOfType<SCC_Camera> ().distance;
		} else {
			if (defaultSpawnPoint) {
				spawnPoint = defaultSpawnPoint.position;
				spawnRot = defaultSpawnPoint.rotation;
			} else {
				spawnPoint = Vector3.zero;
				spawnRot = Quaternion.identity;
			}
		}

		GameObject playerCar = GameObject.Instantiate (spawnableCars[selectedCar], spawnPoint, spawnRot) as GameObject;

		if(GameObject.FindObjectOfType<SCC_Camera> ())
			GameObject.FindObjectOfType<SCC_Camera> ().playerCar = playerCar.GetComponent<SCC_Drivetrain>().transform;

		if(GameObject.FindObjectOfType<SCC_Dashboard> ())
			GameObject.FindObjectOfType<SCC_Dashboard> ().car = playerCar.GetComponent<SCC_Drivetrain>();
	
	}

}
