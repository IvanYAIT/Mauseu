using System;
using Services.Inventory.Items;

namespace Services.Weapons
{
    public class UserWeaponData
    {
        public ItemType Type;
        public int Level;
        public Guid Id;

        public UserWeaponData(ItemType type, int level, Guid id)
        {
            Type = type;
            Level = level;
            Id = id;
        }
    }
}