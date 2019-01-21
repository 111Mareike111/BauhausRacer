using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHuggingVehicle : MonoBehaviour {

    public GameObject carModel;
    public Transform raycastPoint;

    private float hoverHeight = 0f;
    private float speed = 90.0f;

    private float terrainHeight;
    private float rotationAmount;
    private float powerAmount;
    private RaycastHit hit;
    private Vector3 pos;
    private Vector3 forwardDirection;

    void Update()
    {
        // Keep at specific height above terrain
        pos = transform.position;
        Ray ray = new Ray(transform.position,-transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,hoverHeight))
        {
            float proportionalHeight = ( hoverHeight - hit.distance ) / hoverHeight;
            //Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            //carRigitBody.AddForce(appliedHoverForce,ForceMode.Acceleration);
            transform.position = new Vector3(pos.x,
                                         proportionalHeight,
                                         pos.z);
        }
        else
        {
            //transform.position = new Vector3(pos.x,pos.y - ( Time.deltaTime * 9.81f ),pos.z);
        }

        //float terrainHeight = Terrain.activeTerrain.SampleHeight(pos);
        

        // Rotate to align with terrain
        Physics.Raycast(raycastPoint.position,Vector3.down,out hit);
        transform.up -= ( transform.up - hit.normal ) * 0.1f;

        // Rotate with input
        rotationAmount = Input.GetAxis("Horizontal_Hover") * 120.0f;
        rotationAmount *= Time.deltaTime;
        carModel.transform.Rotate(0.0f,rotationAmount,0.0f);

        // Move forward
        powerAmount = Input.GetAxis("Vertical_Hover");
        forwardDirection = carModel.transform.forward;
        transform.position += forwardDirection * Time.deltaTime * powerAmount * speed;
    }


}
