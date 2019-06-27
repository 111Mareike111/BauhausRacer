using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedlines : MonoBehaviour
{
    public ParticleSystem SpeedlineEffectSystem;

    [Header("Effect intensity")] 
    [Range(0,1f)]
    public float Intensity;


    [Header("Effect bounds")]
    public float MinSpeed = 0;
    public float MaxSpeed = 6;

    public float MinRate = 0;
    public float MaxRate = 100;

    public float MinLife = 0;
    public float MaxLife = 10;

    private void OnValidate()
    {
        SetEffectValues();
    }

    private void SetEffectValues()
    {
        var mainModule = SpeedlineEffectSystem.main;
        
        //set speed
        var speed = GetEffectValueByIntensity(MinSpeed, MaxSpeed);
        mainModule.startSpeed = speed;

        //set rate
        var rate = GetEffectValueByIntensity(MinRate, MaxRate);
        var emissionModule = SpeedlineEffectSystem.emission;
        emissionModule.rateOverTime = rate;
        
        //set life
        var life = GetEffectValueByIntensity(MinLife, MaxLife, true);
        mainModule.startLifetime = life;

    }

    private float GetEffectValueByIntensity(float minVal, float maxVal, bool isInverted = false)
    {
        var intensity = Intensity;

        if (isInverted)
        {
            intensity = 1 - Intensity;
        }
        
        return (maxVal - minVal) * intensity + minVal;
    }
}
