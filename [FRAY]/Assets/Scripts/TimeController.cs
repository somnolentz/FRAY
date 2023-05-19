using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TimeController : MonoBehaviour
{
    #region main variables
    [SerializeField]
    private float timeMultiplier;

    [SerializeField]
    private float startHour;

    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;


    private DateTime currentTime;

    private TimeSpan sunriseTime;

    private TimeSpan sunsetTime;

    private bool isNight;



    #endregion
    #region ambient lighting variables
    [SerializeField]
    private Color dayAmbientLight;

    [SerializeField]
    private Color nightAmbientLight;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private float maxSunLightIntensity;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private float maxMoonLightIntensity;
    #endregion
    #region stars

    [SerializeField]
    private Transform starTransform;

    [SerializeField]
    private Vector3 StarsLight = new Vector3(19f, 30f, 0f), StarsExtinguish = new Vector3(03f, 30f, 0f);

    [SerializeField]
    private float starsFadingIn = 7200f, starsFadeOut = 7200f;

    [SerializeField]
    private float starSpeed;

    private float fade, timeLight, timeExtinguish;

    private Color tintColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    private Renderer rend;

    #endregion
    void Start()
    {
       
        rend = starTransform.GetComponent<ParticleSystem>().GetComponent<Renderer>();


        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour); //sets starting hour
        sunriseTime = TimeSpan.FromHours(sunriseHour); //sunrise hour
        sunsetTime = TimeSpan.FromHours(sunsetHour); //sunset hour

        starsFadingIn /= timeMultiplier;
        starsFadeOut /= timeMultiplier;
    }


    void Update()
    { 
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        showStars();
    }


    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier); //makes time passs
    }

    private void RotateSun()
    {
        float sunLightRotation;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime) //checks if its daytime
        {
            isNight = false;
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes; //checks amt of time that has passed

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage); //rotates sun smoothly to 180deg


        }
        else //handles nighttime
        {

            isNight = true;

            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);

            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            starTransform.Rotate(new Vector3(15, 30, 45) * starSpeed * Time.deltaTime);

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage); //rotates sun 180 to 360, causing night
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }


    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);  // forward and down direction of sun calculation, gives value between (- 1. 0, 1) 

        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct)); //sets to 0 to max, uses dotproduct as interpolation value.

        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct)); //max to 0, moon will transition on as sun goes to off

        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct)); //transitions between lights smoothy using lightchangecurve 
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime) //checks difference between time
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }


    private void showStars()
    {

        if (isNight == true)
        {
            fade += Time.deltaTime / starsFadingIn;
            if (fade > 1f) fade = 1f;
        }
        else
        {
            fade -= Time.deltaTime / starsFadeOut;
            if (fade < 0f) fade = 0f;

        }
        tintColor.a = fade;
        rend.material.SetColor("_TintColor", tintColor);

    }
   










}

