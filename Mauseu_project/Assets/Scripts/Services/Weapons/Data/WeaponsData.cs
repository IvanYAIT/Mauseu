using System.Linq;
using Services.Inventory.Items;
using UnityEngine;

namespace Services.Weapons.Data
{
    [CreateAssetMenu(fileName = "WeaponsData", menuName = "Data/WeaponsData", order = 1)]
    public class WeaponsData : ScriptableObject
    {
        [SerializeField] private WeaponData[] _weapons;

        public WeaponData GetByType(ItemType type) => _weapons.First(w => w.WeaponType == type);
    }
}