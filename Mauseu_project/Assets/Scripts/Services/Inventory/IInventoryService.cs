using Services.Inventory.Items;

namespace Services.Inventory
{
    public interface IInventoryService
    {
        public InventoryData GetAllItems();

        public void AddItem(ItemType type, int amount);
        public void AddItem(ItemType type);
        
        public void RemoveItem(ItemType type, int amount);
        public void RemoveItem(ItemType type);
    }
}