using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSonarAbility : MonoBehaviour
{
    [SerializeField] private Light pointLight;

    [Header("Light Intensity Settings:")]
    [SerializeField] private float defaultLightIntensity = 1f;
    [SerializeField] private float maxLightIntensity = 5f;

    [Header("Light Range Settings:")]
    [SerializeField] private int defaultLightRange = 10;
    [SerializeField] private int maxLightRange = 200;
    
    [SerializeField] private float illuminateTime = 1f;

    private float illuminateCountdown = 0f;

    private float intensityToChangeTo = 1f;
    private float rangeToChangeTo = 10f;

    private bool canSonar = true;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canSonar){
            print("Changing light");
            canSonar = false;
            intensityToChangeTo = maxLightIntensity;
            rangeToChangeTo = maxLightRange;
            illuminateCountdown = illuminateTime;
        }

        illuminateCountdown -= Time.deltaTime;
        if(illuminateCountdown <= 0f){
            intensityToChangeTo = defaultLightIntensity;
            rangeToChangeTo = defaultLightRange;
        }

        
        pointLight.intensity = Mathf.Lerp(pointLight.intensity, intensityToChangeTo, Time.deltaTime);
        pointLight.range = Mathf.Lerp(pointLight.range, rangeToChangeTo, Time.deltaTime * 5f);
        
        if(defaultLightRange == (int)pointLight.range){
            canSonar = true;
        }
        
    }
}
