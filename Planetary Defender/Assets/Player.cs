using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] [Tooltip("In ms^-1")] float xSpeed = 5f;
    [SerializeField] [Tooltip("In ms^-1")] float ySpeed = 5f;
    [SerializeField] float xMaxOffset = 3f;
    [SerializeField] float yMaxOffset = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        RespondToHorizontalInput();
        RespondToVerticalInput();
    }

    private void RespondToHorizontalInput()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xMaxOffset, xMaxOffset);

        transform.localPosition = new Vector3(newXPos, transform.localPosition.y, transform.localPosition.z);
    }

    private void RespondToVerticalInput()
    {
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float newYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -yMaxOffset, yMaxOffset);

        transform.localPosition = new Vector3(transform.localPosition.x, newYPos, transform.localPosition.z);
    }
}
