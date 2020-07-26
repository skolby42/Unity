using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpCamera = null;
    [SerializeField] RigidbodyFirstPersonController fpController = null;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomedOutMouseSpeed = 2f;
    [SerializeField] float zoomedInMouseSpeed = 0.5f;

    private void OnDisable()
    {
        ZoomOut();
    }

    void Update()
    {
        ProcessMouseInput();
    }

    private void ProcessMouseInput()
    {
        if (CrossPlatformInputManager.GetButtonDown("Zoom"))
        {
            ZoomIn();
        }

        if (CrossPlatformInputManager.GetButtonUp("Zoom"))
        {
            ZoomOut();
        }
    }

    private void ZoomOut()
    {
        fpCamera.fieldOfView = zoomedOutFOV;
        fpController.mouseLook.XSensitivity = zoomedOutMouseSpeed;
        fpController.mouseLook.YSensitivity = zoomedOutMouseSpeed;
    }

    private void ZoomIn()
    {
        fpCamera.fieldOfView = zoomedInFOV;
        fpController.mouseLook.XSensitivity = zoomedInMouseSpeed;
        fpController.mouseLook.YSensitivity = zoomedInMouseSpeed;
    }
}
