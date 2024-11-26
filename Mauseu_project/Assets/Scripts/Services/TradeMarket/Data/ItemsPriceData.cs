using System.Linq;
using Services.Inventory.Items;

namespace Services.TradeMarket.Data
{
    public class ItemsPriceData
    {
        private readonly ItemPriceData[] _items;

        public ItemsPriceData(ItemPriceData[] items) => _items = items;

        public ItemPriceData[] GetAllItems() => _items;

        public int GetPrice(ItemType type) => _items.First(i => i.Type == type).Cost;

        public ItemPriceData GetPriceData(ItemType type) => _items.First(i => i.Type == type);

        public ItemPriceData[] GetItemsInCategory(ItemCategory category) =>
            _items.Where(i => i.Category == category).ToArray();

        public ItemPriceData[] GetItemsInCategory(ItemCategory category, ItemType exceptItem) =>
            _items.Where(i => i.Category == category && i.Type != exceptItem).ToArray();

        public void ModifyCost(ItemType type, int newCost) =>
            _items.First(i => i.Type == type).Cost = newCost;
    }
}