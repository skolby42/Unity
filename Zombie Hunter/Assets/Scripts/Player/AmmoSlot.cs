using System;
using UnityEngine;

public partial class Ammo
{
    [Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
        public int maxAmmo = 10;

        public void AddAmmo(int newAmount)
        {
            ammoAmount = Mathf.Clamp(ammoAmount + newAmount, 0, maxAmmo);
        }
    }
}
