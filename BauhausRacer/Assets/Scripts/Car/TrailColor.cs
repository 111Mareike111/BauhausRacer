using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailColor : MonoBehaviour {
    private ParticleSystem.MainModule settings;
    [SerializeField] private ParticleSystem particleSystem;

    public void ChangeTrailColor(Color color)
    {
        settings = particleSystem.main;
        color.a = 1;
        settings.startColor = color;
        
    }
    
	
	
}
