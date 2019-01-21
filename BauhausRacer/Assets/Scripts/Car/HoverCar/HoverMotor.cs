using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMotor : MonoBehaviour {
    [SerializeField] private float speed = 90f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float hoverForce = 65f;
    [SerializeField] private float hoverHight = 3.5f;

    private float powerInput;
    private float turnInput;
    private Rigidbody carRigitBody;

    private void Awake()
    {
        carRigitBody = GetComponent<Rigidbody>();
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        powerInput = Input.GetAxis("Vertical_Hover");
        turnInput = Input.GetAxis("Horizontal_Hover");
	}

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position,-transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,hoverHight))
        {
            float proportionalHeight = ( hoverHight - hit.distance ) / hoverHight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            carRigitBody.AddForce(appliedHoverForce,ForceMode.Acceleration);
        }

        carRigitBody.AddRelativeForce(0f,0f,powerInput * speed);
        carRigitBody.AddRelativeTorque(0f,turnInput * turnSpeed, 0f);
    }
}
