using Services.Inventory.Items;

namespace Services.Weapons.Data
{
    [System.Serializable]
    public class WeaponData
    {
        public ItemType WeaponType;
        public CharacteristicsData CharacteristicsData;
        public UpgradeData LevelsData;
    }
}