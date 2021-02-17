using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSystem : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 1.0f;
    [SerializeField] float minAngle = 5.0f;

    Light myLight;

    private void Start()
    {
        myLight = GetComponent<Light>();
    }

    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
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
