using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;

    private void Start()
    {
        SetActiveWeapon();
    }

    private void Update()
    {
        int previousWeapon = currentWeapon;
        ProcessKeyInput();
        ProcessScrollWheel();

        if (currentWeapon != previousWeapon)
        {
            SetActiveWeapon();
        }
    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ProcessScrollWheelUp();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ProcessScrollWheelDown();
        }
    }

    private void ProcessScrollWheelUp()
    {
        if (currentWeapon >= transform.childCount - 1)
        {
            currentWeapon = 0;
        }
        else
        {
            currentWeapon++;
        }
    }

    private void ProcessScrollWheelDown()
    {
        if (currentWeapon <= 0)
        {
            currentWeapon = transform.childCount - 1;
        }
        else
        {
            currentWeapon--;
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentWeapon = 4;
        }
    }

    private void SetActiveWeapon()
    {
        int weaponIndex = 0;

        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(weaponIndex == currentWeapon);
            weaponIndex++;
        }
    }
}
