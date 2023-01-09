using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DayCycleController : MonoBehaviour
{
    [Range(0, 24)] public float timeOfDay;

    public GameObject sun;
    public Light sunLight;

    public float timeMultiplier;
    
    void Start()
    {
        sun = gameObject;
        sunLight = gameObject.GetComponent<Light>();
    }

    private void FixedUpdate()
    {
        timeOfDay += Time.fixedDeltaTime * timeMultiplier;
        if (timeOfDay > 24) timeOfDay = 0;
        UpdateTime();
    }

    private void OnValidate()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        float alpha = timeOfDay / 24f;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        sun.transform.rotation = Quaternion.Euler(sunRotation, -150f, 0);
    }
}
