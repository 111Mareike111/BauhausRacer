using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public Transform target;
    public float waitTime = 2f;
    public float lampTime = 1f;
    private float timer;
    public float smoothTime = 0.3F;
    public float rotationSpeed = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public IlluminaitonBehaviour[] illuminaitonBehaviours;
    enum StatusZoom { s_01wait, s_02zoom, s_03countdown, s_04startRace};
    enum StatusLights { three, two, one, go};
    private StatusZoom statusCameraZoom;
    private StatusLights statusLights;
    public Camera carCamera;

    // Use this for initialization
    void Start () {
        timer = waitTime;
        statusCameraZoom = StatusZoom.s_01wait;
	}
	
	// Update is called once per frame
	void Update () {

        switch (statusCameraZoom)
        {
            case StatusZoom.s_01wait:
                WaitForTimer();
                break;
            case StatusZoom.s_02zoom:
                ZoomToPosition();
                break;
            case StatusZoom.s_03countdown:
                CountDown();
                break;
        }
        
    }

    private void CountDown()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if(statusLights == StatusLights.three)
            {
                ChangeLampStatus(StatusLights.two,0,1,false,true);
            }else if (statusLights == StatusLights.two)
            {
                ChangeLampStatus(StatusLights.one,1,2,false,true);
            }else if (statusLights == StatusLights.one)
            {
                ChangeLampStatus(StatusLights.go,0,1,true,true);
                statusCameraZoom = StatusZoom.s_04startRace;
                StartRace();
            }
            timer = lampTime;
        }
    }

    private void ChangeLampStatus(StatusLights value, int int1, int int2, bool bool1,bool bool2)
    {
        statusLights = value;
        illuminaitonBehaviours[int1].GlowMaterial(bool1);
        illuminaitonBehaviours[int2].GlowMaterial(bool2);
    }

    private void WaitForTimer()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            statusCameraZoom = StatusZoom.s_02zoom;
        }
    }

    private void ZoomToPosition()
    {
        transform.position = Vector3.SmoothDamp(transform.position,target.position,ref velocity,smoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation,target.rotation,rotationSpeed * Time.deltaTime);
        float distance = Vector3.Distance(transform.position,target.position);
        if(distance < 0.1f)
        {
            statusCameraZoom = StatusZoom.s_03countdown;
            statusLights = StatusLights.three;
            illuminaitonBehaviours[0].GlowMaterial(true);
            timer = lampTime;
        }
        
    }

    private void StartRace()
    {
        carCamera.transform.position = transform.position;
        carCamera.transform.rotation = transform.rotation;
        carCamera.enabled = true;
        this.gameObject.SetActive(false);
        //ToDo PrepareRaceStart
    }


}
