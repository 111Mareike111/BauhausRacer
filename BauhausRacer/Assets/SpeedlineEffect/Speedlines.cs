using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class Speedlines : MonoBehaviour
{
    public ParticleSystem SpeedlineEffectSystem;
    [SerializeField] private CarController carController;
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

    private void Update()
    {
        SetIntesityByCarSpeed();
        SetEffectValues();
    }

    private void SetIntesityByCarSpeed()
    {
        if(carController.MaxSpeed == 0)
        {
            Intensity = 0;
        }
        else
        {
            Intensity = Mathf.Clamp(carController.CurrentSpeed / carController.MaxSpeed, 0, 1);
        }
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
