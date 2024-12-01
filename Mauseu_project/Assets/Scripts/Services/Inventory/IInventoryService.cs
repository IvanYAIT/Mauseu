using System;
using Services.Inventory.Data;
using Services.Inventory.Items;

namespace Services.Inventory
{
    public interface IInventoryService
    {
        public StackableItems GetAllStackableItems();
        public UniqItems GetAllUniqItems();

        public int GetAmount(ItemType type);
        
        public void AddItem(ItemType type, int amount);
        public void AddItem(ItemType type);
        public void AddItem(ItemType type, Guid id);
        
        public void RemoveItem(ItemType type, int amount);
        public void RemoveItem(ItemType type);
        public void RemoveItem(Guid id);
    }
}