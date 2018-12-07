using UnityEngine;
using System.Collections;

[AddComponentMenu("BoneCracker Games/Simple Car Controller/Car Camera")]
public class SCC_Camera : MonoBehaviour {

	public Transform playerCar;

	public float distance = 20.0f;
	public float height = 5.0f;
	public float heightDamping = 2.0f;

	public bool useCameraCollision = true;
	public float closerRadius  = 0.2f;
	public float closerSnapLag  = 0.2f;
	public float lookAtHeight = 0.0f;
	
	public float rotationSnapTime = 0.3F;
	
	public float distanceSnapTime;
	public float distanceMultiplier;
	
	private Vector3 lookAtVector;
	
	private float usedDistance;

	float wantedRotationAngle;
	float wantedHeight;
	
	float currentRotationAngle;
	float currentHeight;
	
	private Quaternion currentRotation;
	private Vector3 wantedPosition;

	private float currentDistance = 0.0f;
	private float yVelocity = 0.0F;
	private float zVelocity = 0.0F;
	private float targetDistance = 0.0f;

	void Start () {

		lookAtVector =  new Vector3(0,lookAtHeight,0);
		currentDistance = distance;

	}
	
	void LateUpdate () {

		if (!playerCar)
			return;
		
		wantedHeight = playerCar.position.y + height;
		
		wantedRotationAngle = playerCar.eulerAngles.y;
		currentRotationAngle = transform.eulerAngles.y;
		
		currentRotationAngle = Mathf.SmoothDampAngle(currentRotationAngle, wantedRotationAngle, ref yVelocity, rotationSnapTime);

		if (useCameraCollision) {

			RaycastHit hit;

			if (Physics.Raycast (playerCar.position, transform.TransformDirection (-Vector3.forward), out hit, distance) && !hit.transform.IsChildOf (playerCar))
				targetDistance = hit.distance - closerRadius;
			else
				targetDistance = distance;

		} else {

			targetDistance = distance;

		}

		currentDistance = Mathf.SmoothDamp(currentDistance, targetDistance, ref zVelocity, closerSnapLag * 0.3f);

		wantedPosition = playerCar.position;
		wantedPosition.y = wantedHeight;
		
		wantedPosition += Quaternion.Euler(0, currentRotationAngle, 0) * new Vector3(0, 0, -currentDistance);
		
		transform.position = wantedPosition;
		
		transform.LookAt(playerCar.position + lookAtVector);
		
	}

}
