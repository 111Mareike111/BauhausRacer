using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer
{
    public class TrackPoints : MonoBehaviour
    {
        [SerializeField] TrackPoints[] nextTrackpoints;
        // Use this for initialization
        void Start()
        {
            CheckMissingLinks();
        }

        void CheckMissingLinks()
        {
            if (nextTrackpoints == null || nextTrackpoints.Length == 0)
            {
                PrintErrorLog();
                return;
            }
            foreach(TrackPoints tp in nextTrackpoints)
            {
                try
                {
                    if (tp == null)
                    {
                        PrintErrorLog();
                        return;
                    }
                }
                catch
                {
                    PrintErrorLog();
                }
                
            }
        }

        void PrintErrorLog()
        {
            Debug.LogError(string.Format("{0} hat keinen oder fehlende nächste Punkte!",transform.name));
        }
    }
}