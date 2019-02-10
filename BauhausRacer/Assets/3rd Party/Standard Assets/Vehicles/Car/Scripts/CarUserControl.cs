using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using BauhausRacer;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        private float h;
        private float v;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            if(!Game.Instance.gameStopped){
                         // pass the input to the car!
                if(BauhausRacer.Game.Instance.wheel){
                    h = CrossPlatformInputManager.GetAxis("Horizontal2");
                    v = CrossPlatformInputManager.GetAxis("Vertical2");
                } else {
                    h = CrossPlatformInputManager.GetAxis("Horizontal");
                    v = CrossPlatformInputManager.GetAxis("Vertical");
                }
            }
       

         
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
