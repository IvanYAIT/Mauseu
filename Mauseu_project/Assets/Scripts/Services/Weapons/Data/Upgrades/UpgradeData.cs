using UnityEngine;

namespace Services.Weapons.Data
{
    [CreateAssetMenu(fileName = "Upgrades", menuName = "Data/Weapon/Upgrades", order = 1)]
    public class UpgradeData : ScriptableObject
    {
        [SerializeField] private UpgradeCost[] _levels;
        
        public UpgradeCost GetByLevel(int level) => _levels[level];
    }
}