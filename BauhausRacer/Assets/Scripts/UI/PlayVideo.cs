using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayVideo : MonoBehaviour {


    public MovieTexture movieTexture;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        GetComponent<RawImage>().texture = movieTexture as MovieTexture;
        movieTexture.loop = true;
        movieTexture.Play();
	}

    private void OnEnable()
    {
        movieTexture.Play();
    }
}
