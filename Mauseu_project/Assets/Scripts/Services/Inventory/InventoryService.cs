using System;
using System.Linq;
using Services.Inventory.Commands;
using Services.Inventory.Data;
using Services.Inventory.Items;

namespace Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly InventoryData _inventoryData;
        private readonly StackableItems _stackableItems;
        private readonly UniqItems _uniqItems;
        
        public InventoryService(InventoryData inventoryData)
        {
            _inventoryData = inventoryData;
            _stackableItems = inventoryData.StackableItems;
            _uniqItems = inventoryData.UniqItems;
        }

        public StackableItems GetAllStackableItems() => _stackableItems;

        public UniqItems GetAllUniqItems() => _uniqItems;

        public int GetAmount(ItemType type) => _stackableItems[type];

        public void AddItem(ItemType type, int amount)
        {
            if (!_stackableItems.ContainsKey(type))
                _stackableItems.Add(type, amount);

            _stackableItems[type] += amount;

            Save();
        }

        public void AddItem(ItemType type) => AddItem(type, 1);

        public void AddItem(ItemType type, Guid id)
        {
            var newUniqItem = new UniqItem(type, id);
            _uniqItems.Add(newUniqItem);

            Save();
        }

        public void RemoveItem(ItemType type, int amount)
        {
            _stackableItems[type] -= amount;
            Save();
        }

        public void RemoveItem(ItemType type) => RemoveItem(type, 1);

        public void RemoveItem(Guid id)
        {
            var item = _uniqItems.First(i => i.Id == id);
            _uniqItems.Remove(item);
            
            Save();
        }

        private void Save()
        {
            new SaveInventoryDataCommand(_inventoryData).Execute();
        }
    }
}