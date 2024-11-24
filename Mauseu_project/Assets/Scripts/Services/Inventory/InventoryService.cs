using Services.Inventory.Commands;
using Services.Inventory.Items;

namespace Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly InventoryData _items;

        public InventoryService(InventoryData data) => _items = data;

        public InventoryData GetAllItems() => _items;

        public void AddItem(ItemType type, int amount)
        {
            if (!_items.ContainsKey(type))
                _items.Add(type, amount);

            _items[type] += amount;
            
            Save();
        }

        public void AddItem(ItemType type) => AddItem(type, 1);

        public void RemoveItem(ItemType type, int amount)
        {
            _items[type] -= amount;
            Save();
        }

        public void RemoveItem(ItemType type) => RemoveItem(type, 1);

        private void Save()
        {
            new SaveInventoryDataCommand(_items).Execute();
        }
    }
}