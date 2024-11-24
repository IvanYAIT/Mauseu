using System.Linq;
using Services.Inventory.Items;
using UnityEngine;

namespace Services.Inventory.Monsters
{
    [CreateAssetMenu(fileName = "MonstersData", menuName = "Data/MonstersData", order = 2)]
    public class MonstersData : ScriptableObject
    {
        [SerializeField] private MonsterData[] _monsters;

        public MonsterData GetDataByType(ItemType type)
        {
            return _monsters.First(m => m.ItemType == type);
        }
    }
}