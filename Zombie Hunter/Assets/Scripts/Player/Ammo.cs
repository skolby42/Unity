using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);
        if (ammoSlot == null) return 0;

        return ammoSlot.ammoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType, int value = 1)
    {
        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);
        if (ammoSlot == null) return;

        ammoSlot.ammoAmount = Mathf.Clamp(ammoSlot.ammoAmount - value, 0, ammoSlot.maxAmmo);
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int newAmount)
    {
        AmmoSlot slot = GetAmmoSlot(ammoType);
        if (slot == null) return;

        slot.AddAmmo(newAmount);
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (var slot in ammoSlots)
        {
            if (slot.ammoType == ammoType) return slot;
        }
        return null;
    }
}
