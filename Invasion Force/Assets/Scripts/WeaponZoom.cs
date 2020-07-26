using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpCamera = null;
    [SerializeField] RigidbodyFirstPersonController fpController = null;
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
            fpController.mouseLook.XSensitivity = zoomedMouseSpeed;
            fpController.mouseLook.YSensitivity = zoomedMouseSpeed;
        }

        if (CrossPlatformInputManager.GetButtonUp("Zoom"))
        {
            fpController.mouseLook.XSensitivity = defaultMouseSpeed;
            fpController.mouseLook.YSensitivity = defaultMouseSpeed;
        }
    }
}
