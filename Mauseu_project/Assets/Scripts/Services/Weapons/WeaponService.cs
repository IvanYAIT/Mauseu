using System;
using System.Collections.Generic;
using System.Linq;
using Services.Inventory.Items;
using Services.Weapons.Commands;
using Services.Weapons.Data;

namespace Services.Weapons
{
    public class WeaponService : IWeaponService
    {
        private readonly List<UserWeaponData> _userWeapons;
        private readonly WeaponsData _weaponsData;

        public WeaponService(List<UserWeaponData> userWeapons, WeaponsData weaponsData)
        {
            _userWeapons = userWeapons;
            _weaponsData = weaponsData;
        }

        public void Add(ItemType type, int level, Guid id)
        {
            var newWeapon = new UserWeaponData(type, level, id);
            _userWeapons.Add(newWeapon);
            Save();
        }

        public void Remove(Guid id)
        {
            var targetWeapon = GetById(id);
            _userWeapons.Remove(targetWeapon);
            Save();
        }

        public void Upgrade(Guid id)
        {
            var targetWeapon = GetById(id);
            targetWeapon.Level++;
            Save();
        }

        public UserWeaponData GetById(Guid id) => _userWeapons.First(w => w.Id == id);

        public List<UserWeaponData> GetAllUserWeapon() => _userWeapons;

        public Characteristic GetCharacteristic(ItemType type, int level) =>
            _weaponsData.GetByType(type).CharacteristicsData.GetByLevel(level);

        public UpgradeCost GetUpgradeCost(ItemType type, int level) =>
            _weaponsData.GetByType(type).LevelsData.GetByLevel(level);

        public int GetCurrentLevel(Guid id) => _userWeapons.First(w => w.Id == id).Level;

        public bool IsMaxLevel(Guid id)
        {
            var currentLevel = GetCurrentLevel(id);
            var type = GetById(id).Type;
            var maxLevel = _weaponsData.GetByType(type).CharacteristicsData.GetLevelsAmount();
            return currentLevel == maxLevel;
        }

        private void Save()
        {
            new SaveWeaponDataCommand(_userWeapons).Execute();
        }
    }
}