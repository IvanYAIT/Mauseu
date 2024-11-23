using System.Collections.Generic;
using Services.Inventory.Items;

namespace Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly Dictionary<ItemType, int> _items;

        public InventoryService(Dictionary<ItemType, int> data) => _items = data;

        public Dictionary<ItemType, int> GetAllItems() => _items;

        public void AddItem(ItemType type, int amount)
        {
            if (!_items.ContainsKey(type))
                _items.Add(type, amount);

            _items[type] += amount;
        }

        public void AddItem(ItemType type)
        {
            AddItem(type, 1);
        }
    }
}