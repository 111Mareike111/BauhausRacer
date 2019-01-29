using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTestsript : MonoBehaviour {
    bool inTransition = false;
    int toggle = 1;
    public Material material;
    public float changespeed = 1;
    public float changevalue = 1.5f;
    Color curerntColor;
    public Color newColor;
    public Color startColor;

	// Use this for initialization
	void Start () {
        curerntColor = startColor;
        material.SetColor("Color_ABE336C8",curerntColor);
        material.SetColor("Color_6066237A",curerntColor);
    }

    // Update is called once per frame
    void Update () {
        if (inTransition)
        {
            ExperimentalColorChange();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PrepareTransition();
            }
        }
    }

    public void PrepareTransition(Color color)
    {
        if (toggle == 1)
        {
            material.SetColor("_Color_Transition",color);
        }
        else
        {
            material.SetColor("_Color_Surface",color);
        }

        inTransition = true;
        toggle *= -1;
    }

    void PrepareTransition()
    {
        if (toggle == 1)
        {
            material.SetColor("_Color_Transition",newColor);
        }
        else
        {
            material.SetColor("_Color_Surface",newColor);
        }
        
        inTransition = true;
        toggle *= -1;
    }

    void ExperimentalColorChange()
    {
        changevalue = changevalue + changespeed * toggle * Time.deltaTime;
        material.SetFloat("_Vector1_Transition",changevalue);
        
        if((changevalue > 1.5 && toggle == 1) || (changevalue < -0.5 && toggle == -1))
        {
            inTransition = false;
        }
    }

    private void OnApplicationQuit()
    {
        curerntColor = startColor;
        material.SetColor("_Color_Surface",curerntColor);
        material.SetColor("_Color_Transition",curerntColor);
    }
}
