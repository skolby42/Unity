using System;

public partial class Ammo
{
    [Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
        public int maxAmmo = 10;
    }
}
