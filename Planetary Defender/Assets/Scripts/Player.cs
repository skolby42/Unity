using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] [Tooltip("In ms^-1")] float movementSpeed = 5f;
    [SerializeField] float xMaxOffset = 3f;
    [SerializeField] float yMaxOffset = 2.5f;
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -15f;

    void Start()
    {
        
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessTranslation()
    {
        float xOffset = CrossPlatformInputManager.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float yOffset = CrossPlatformInputManager.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xMaxOffset, xMaxOffset);
        float newYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -yMaxOffset, yMaxOffset);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float positionPitch = transform.localPosition.y * positionPitchFactor;
        float positionYaw = transform.localPosition.x * positionYawFactor;

        float controlPitch = CrossPlatformInputManager.GetAxis("Vertical") * controlPitchFactor;
        float controlRoll = CrossPlatformInputManager.GetAxis("Horizontal") * controlRollFactor;

        transform.localRotation = Quaternion.Euler(positionPitch + controlPitch, positionYaw, controlRoll);
    }
}
