using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;
    [SerializeField] int maxAmmo = 10;

    public int GetCurrentAmmo()
    {
        return ammoAmount;
    }

    public void ReduceCurrentAmmo(int value = 1)
    {
        ammoAmount = Mathf.Clamp(ammoAmount - value, 0, maxAmmo);
    }
}
