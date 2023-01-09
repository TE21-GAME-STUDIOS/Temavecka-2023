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

    public GameObject moon;
    public Light moonLight;

    public float timeMultiplier = 1f;

    public bool isNight;

    private void Start()
    {
        sun = GameObject.Find("Sun");
        sunLight = sun.GetComponent<Light>();

        moon = GameObject.Find("Moon");
        moonLight = moon.GetComponent<Light>();
    }

    private void FixedUpdate()
    {
        timeOfDay += Time.fixedDeltaTime * timeMultiplier;
        if (timeOfDay > 24) timeOfDay = 0;
        UpdateTime();
        CheckNightDayTransition();
    }

    private void OnValidate()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        float alpha = timeOfDay / 24f;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        float moonRotation = sunRotation - 180f;
        sun.transform.rotation = Quaternion.Euler(sunRotation, -150f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRotation, -150f, 0);
    }

    private void CheckNightDayTransition()
    {
        if (isNight)
        {
            if (moon.transform.rotation.eulerAngles.x > 180) StartDay();
        }
        else
        {
            if(sun.transform.rotation.eulerAngles.x > 180) StartNight();
        }
    }

    private void StartDay()
    {
        isNight = false;
        moonLight.shadows = LightShadows.None;
        sunLight.shadows = LightShadows.Soft;
    }

    private void StartNight()
    {
        isNight = true;
        sunLight.shadows = LightShadows.None;
        moonLight.shadows = LightShadows.Soft;
    }
}