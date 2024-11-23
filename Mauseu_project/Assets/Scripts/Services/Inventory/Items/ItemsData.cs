using System.Linq;
using UnityEngine;

namespace Services.Inventory.Items
{
    [CreateAssetMenu(fileName = "ItemsData", menuName = "Data/ItemsData", order = 1)]
    public class ItemsData : ScriptableObject
    {
        [SerializeField] private ItemData[] _items;

        public ItemData GetItemData(ItemType type)
        {
            return _items.First(i => i.Type == type);
        }
    }
}