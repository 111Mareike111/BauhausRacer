using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCar : MonoBehaviour {
    Rigidbody m_body;
    float m_deadZone = 0.1f;

    public float m_forwardAcl = 100f;
    public float m_backwardAcl = 25f;
    float m_currThrust = 0f;

    public float m_turnStrength = 10f;
    float m_currTurn = 0f;

    int m_layerMask;
    public float m_hoverForce = 9.0f;
    public float m_hoverHeight = 2.0f;
    public GameObject[] m_hoverPoints;

	// Use this for initialization
	void Start () {
        m_body = GetComponent<Rigidbody>();

        m_layerMask = 1 << LayerMask.NameToLayer("Characters");
        m_layerMask = ~m_layerMask;
	}
	
	// Update is called once per frame
	void Update () {
        //MainThrust
        m_currThrust = 0f;
        float aclAxis = Input.GetAxis("Vertical_Hover");
        if(aclAxis > m_deadZone)
        {
            m_currThrust = aclAxis * m_forwardAcl;
        }else if(aclAxis < -m_deadZone)
        {
            m_currThrust = aclAxis * m_backwardAcl;
        }

        //Turning
        m_currTurn = 0f;
        float turnAxis = Input.GetAxis("Horizontal_Hover");
        if (Mathf.Abs(turnAxis) > m_deadZone)
        {
            m_currTurn = turnAxis;
        }
	}

    private void FixedUpdate()
    {
        //HoverForce
        RaycastHit hit;
        foreach(GameObject hoverPoint in m_hoverPoints)
        {
            if(Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit,m_hoverHeight,m_layerMask))
            {
                m_body.AddForceAtPosition(Vector3.up * m_hoverForce * ( 1.0f - ( hit.distance / m_hoverHeight ) ),hoverPoint.transform.position);
            }
            else
            {
                if(transform.position.y > hoverPoint.transform.position.y)
                {
                    m_body.AddForceAtPosition(hoverPoint.transform.up * m_hoverForce,hoverPoint.transform.position);
                }
                else
                {
                    m_body.AddForceAtPosition(hoverPoint.transform.up * -m_hoverForce,hoverPoint.transform.position);
                }
            }
        }

        //Forward
        if(Mathf.Abs(m_currThrust)> 0)
        {
            m_body.AddForce(transform.forward * m_currThrust);
        }

        //Turn
        if(m_currTurn > 0)
        {
            m_body.AddRelativeTorque(Vector3.up * m_currTurn * m_turnStrength);
        } else if (m_currTurn < 0)
        {
            m_body.AddRelativeTorque(Vector3.up * m_currTurn * m_turnStrength);
        }


    }
}
