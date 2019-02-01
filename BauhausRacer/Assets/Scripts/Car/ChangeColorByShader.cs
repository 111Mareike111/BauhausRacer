using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorByShader : MonoBehaviour {

    bool inTransition = false;
    int toggle = 1;
    public Material material;
    public float changespeed = 1;
    private float changevalue = 1.5f;
    Color curerntColor;
    public Color startColor;

    // Use this for initialization
    void Start()
    {
        curerntColor = startColor;
        material.SetColor("_Color_Transition",curerntColor);
        material.SetColor("_Color_Surface",curerntColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (inTransition)
        {
            ExperimentalColorChange();
        }
    }

    public void PrepareTransition(Color color)
    {
        Debug.Log("JSLAIWEFEOWF");
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

    void ExperimentalColorChange()
    {
        changevalue = changevalue + changespeed * toggle * Time.deltaTime;
        material.SetFloat("_Vector1_Transition",changevalue);

        if (( changevalue > 1.5 && toggle == 1 ) || ( changevalue < -0.5 && toggle == -1 ))
        {
            inTransition = false;
        }
    }

    private void OnApplicationQuit()
    {
        curerntColor = startColor;
        material.SetColor("_Color_Transition",curerntColor);
        material.SetColor("_Color_Surface",curerntColor);
    }
}
