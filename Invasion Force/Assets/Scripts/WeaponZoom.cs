using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpCamera = null;
    [SerializeField] float defaultFOV = 60f;
    [SerializeField] float zoomedFOV = 20f;
    [SerializeField] float defaultMouseSpeed = 2f;
    [SerializeField] float zoomedMouseSpeed = 0.5f;

    void Update()
    {
        UpdateCameraFOV();
        UpdateMouseSensitivity();
    }

    private void UpdateCameraFOV()
    {
        if (CrossPlatformInputManager.GetButtonDown("Zoom"))
        {
            fpCamera.fieldOfView = zoomedFOV;
        }

        if (CrossPlatformInputManager.GetButtonUp("Zoom"))
        {
            fpCamera.fieldOfView = defaultFOV;
        }
    }

    private void UpdateMouseSensitivity()
    {
        if (CrossPlatformInputManager.GetButtonDown("Zoom"))
        {
            var controller = GetComponent<RigidbodyFirstPersonController>();
            controller.mouseLook.XSensitivity = zoomedMouseSpeed;
            controller.mouseLook.YSensitivity = zoomedMouseSpeed;
        }

        if (CrossPlatformInputManager.GetButtonUp("Zoom"))
        {
            var controller = GetComponent<RigidbodyFirstPersonController>();
            controller.mouseLook.XSensitivity = defaultMouseSpeed;
            controller.mouseLook.YSensitivity = defaultMouseSpeed;
        }
    }
}
