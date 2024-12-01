using System;
using System.Collections.Generic;
using Services.Inventory.Items;
using Services.Weapons.Data;

namespace Services.Weapons
{
    public interface IWeaponService
    {
        public void Add(ItemType type, int level);

        public void Remove(Guid id);

        public void Upgrade(Guid id);

        public UserWeaponData GetById(Guid id);

        public List<UserWeaponData> GetAllUserWeapon();

        public Characteristic GetCharacteristic(ItemType type, int level);

        public UpgradeCost GetUpgradeCost(ItemType type, int level);

        public int GetCurrentLevel(Guid id);

        public bool IsMaxLevel(Guid id);
    }
}