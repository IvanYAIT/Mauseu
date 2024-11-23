using System.Collections.Generic;
using Services.Inventory.Items;

namespace Services.Inventory
{
    public interface IInventoryService
    {
        public Dictionary<ItemType, int> GetAllItems();

        public void AddItem(ItemType type, int amount);

        public void AddItem(ItemType type);
    }
}