using System;
using UnityEngine;

namespace BauhausRacer
{
    public class BrakeLight : MonoBehaviour
    {
        public MyCarController car; // reference to the car controller, must be dragged in inspector

        private Renderer m_Renderer;


        private void Start()
        {
            m_Renderer = GetComponent<Renderer>();
        }


        private void Update()
        {
            // enable the Renderer when the car is braking, disable it otherwise.
        //    m_Renderer.enabled = car.Forward < 0f;
        }
    }
}
