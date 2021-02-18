using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSystem : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 1.0f;
    [SerializeField] float minAngle = 5.0f;

    float startAngle;
    float startIntensity;

    Light myLight;

    private void Start()
    {
        myLight = GetComponent<Light>();
        startAngle = myLight.spotAngle;
        startIntensity = myLight.intensity;
    }

    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    public void RestoreLightAngle(float restoreAngle)
    {
        myLight.spotAngle = Mathf.Clamp(myLight.spotAngle + restoreAngle, minAngle, startAngle);
    }

    public void AddLightIntensity(float intensity)
    {
        myLight.intensity = Mathf.Clamp(myLight.intensity + intensity, 0.0f, startIntensity);
    }

    private void DecreaseLightAngle()
    {
        if (myLight.spotAngle <= minAngle) return;

        var angle = myLight.spotAngle - Time.deltaTime * angleDecay;
        myLight.spotAngle = Mathf.Clamp(minAngle, angle, myLight.spotAngle);
    }

    private void DecreaseLightIntensity()
    {
        if (myLight.intensity <= 0) return;

        var intensity = myLight.intensity - Time.deltaTime * lightDecay;
        myLight.intensity = Mathf.Clamp(0f, intensity, myLight.intensity);
    }
}
