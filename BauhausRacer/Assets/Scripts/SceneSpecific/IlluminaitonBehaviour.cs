using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminaitonBehaviour : MonoBehaviour {
    public GameObject standardMaterials;
    public GameObject glowingMaterials;
	// Use this for initialization


    public void GlowMaterial(bool value)
    {
        if (value)
        {
            glowingMaterials.SetActive(true);
            standardMaterials.SetActive(false);
        }
        else
        {
            glowingMaterials.SetActive(false);
            standardMaterials.SetActive(true);
        }
    }
    
}
