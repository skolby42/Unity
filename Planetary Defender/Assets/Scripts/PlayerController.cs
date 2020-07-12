using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] [Tooltip("In ms^-1")] float controlSpeed = 5f;
    [SerializeField] float xMaxOffset = 3f;
    [SerializeField] float yMaxOffset = 2.5f;
    [SerializeField] GameObject[] guns = null;
    [SerializeField] float startControlDelay = 2f;

    [Header("Screen Position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;

    [Header("Control Throw Based")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -15f;

    bool controlsEnabled = false;

    private void Start()
    {
        SetGunsActive(false);
        StartCoroutine(SetControlsEnabled(true, startControlDelay));
    }

    void Update()
    {
        if (controlsEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessTranslation()
    {
        float xOffset = CrossPlatformInputManager.GetAxis("Horizontal") * controlSpeed * Time.deltaTime;
        float yOffset = CrossPlatformInputManager.GetAxis("Vertical") * controlSpeed * Time.deltaTime;

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

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool active)
    {
        foreach (GameObject gun in guns)
        {
            ParticleSystem ps = gun.GetComponent<ParticleSystem>();
            var emission = ps.emission;
            emission.enabled = active;
        }
    }

    private IEnumerator SetControlsEnabled(bool enabled, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        controlsEnabled = enabled;
    }

    private void OnPlayerDeath()
    {
        controlsEnabled = false;
    }
}
