 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer
{
    public class CarTracker : MonoBehaviour
    {

        [SerializeField] private GameObject CarObject;
        [SerializeField] private TrackPoints currentTrackpoints;
        public GameObject turnBack_img;
        public float epsDistance = 10;
        public float inRange = 10;
        private TrackPoints[] _nextTrackpoints;
        private float _lastDistance;
        // Use this for initialization
        void Start()
        {
            GetNextTrackpoints();
        }

        // Update is called once per frame
        void Update()
        {
            //CheckDistence();
            CheckFacing();
        }


        void GetNextTrackpoints()
        {
            _nextTrackpoints = currentTrackpoints.NextTrackpoints;
        }

        public void ChangeCurrentTrackpoints(TrackPoints trackPoints)
        {
            currentTrackpoints = trackPoints;
            GetNextTrackpoints();
        }

        void CheckFacing()
        {
            foreach (TrackPoints nextTrackpoint in _nextTrackpoints)
            {
                var heading = CarObject.transform.position - nextTrackpoint.transform.position;
            }
        }

        void CheckDistence()
        {
            int i = 0;
            int length = _nextTrackpoints.Length;
            foreach(TrackPoints nextTrackpoint in _nextTrackpoints)
            {
                var heading = CarObject.transform.position - nextTrackpoint.transform.position;
                var distance = heading.magnitude;
                //Debug.Log(string.Format("[{0}] distance: {1}", nextTrackpoint.name, distance));
                if ( distance - epsDistance > _lastDistance)
                {
                    //DisplayWarning();
                    i++;
                }
                else
                {
                    HideWarning();
                    
                }
                

                if (distance < inRange)
                {
                    ChangeCurrentTrackpoints(nextTrackpoint);
                }
                _lastDistance = distance;
            }
            if(i == length)
            {
                DisplayWarning();
            }
           

        }

        private void DisplayWarning()
        {
            turnBack_img.SetActive(true);
        }

        private void HideWarning()
        {
            turnBack_img.SetActive(false);
        }
    }
}

