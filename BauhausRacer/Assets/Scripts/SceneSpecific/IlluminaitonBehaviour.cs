using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminaitonBehaviour : MonoBehaviour {
    public Material[] standardMaterials;
    public Material[] glowingMaterials;
    private Renderer renderer;
	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        
	}

    public void GlowMaterial(bool value)
    {
        if (value)
        {
            ChangeMaterials(glowingMaterials);
        }
        else
        {
            ChangeMaterials(standardMaterials);
        }
    }

    private void ChangeMaterials(Material[] materials)
    {
        for(int i = 0; i < renderer.materials.Length; i++)
        {
            renderer.material = materials[i];
        }
    }
}
