using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuBehaviour : MonoBehaviour {

    [SerializeField] private float waitingdelay = 10;
    [SerializeField] private GameObject[] MenuWindows;
    [SerializeField] private GameObject videoWindow;
    [SerializeField] private GameObject playButton;
    private float timer;
	// Use this for initialization
	void Start () {
        timer = Time.realtimeSinceStartup + waitingdelay;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
            SetInitiolSituation();
        else
            CheckTimer();
	}

    void SetInitiolSituation()
    {
        timer = Time.realtimeSinceStartup + waitingdelay;
        if (videoWindow.activeInHierarchy)
        {
            MenuWindows[0].SetActive(true);
            videoWindow.SetActive(false);
            EventSystem.current.SetSelectedGameObject(playButton);
        }
    }

    void CheckTimer()
    {
        if (timer < 0)
        {
            ShowVideo();
        }
        else
        {
            if(Time.realtimeSinceStartup > timer)
            {
                ShowVideo();
            }
            
        }
    }

    void ShowVideo()
    {
        videoWindow.SetActive(true);
        foreach(GameObject gobj in MenuWindows)
        {
            gobj.SetActive(false);
        }
    }
}
