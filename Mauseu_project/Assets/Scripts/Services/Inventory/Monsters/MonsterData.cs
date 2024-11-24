using Services.Inventory.Items;
using UnityEngine;

namespace Services.Inventory.Monsters
{
    [System.Serializable]
    public class MonsterData
    {
        public ItemType ItemType;
        public MonsterType MonsterType;
        public Sprite Icon;
        public string Name;
        public string Description;
    }
}