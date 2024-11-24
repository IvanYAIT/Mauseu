using Services.Inventory.Items;

namespace Services.TradeMarket
{
    public interface ITradeService
    {
        public int GetPrice(ItemType itemType);

        public void SellItem(ItemType itemType);

        public void SellItem(ItemType itemType, int amount);
    }
}