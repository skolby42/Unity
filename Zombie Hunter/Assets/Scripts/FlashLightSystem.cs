using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSystem : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 1.0f;
    [SerializeField] float minAngle = 1.0f;

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
        var angle = myLight.spotAngle - Time.deltaTime * angleDecay;
        Mathf.Clamp(angle, minAngle, myLight.spotAngle);
        myLight.spotAngle = angle;
    }

    private void DecreaseLightIntensity()
    {
        var intensity = myLight.intensity - Time.deltaTime * lightDecay;
        Mathf.Clamp(intensity, 0f, myLight.intensity);
        myLight.intensity = intensity;
    }
}
