using Services.Inventory.Items;
using Services.TradeMarket.Data;

namespace Services.TradeMarket
{
    public interface ITradeService
    {
        public int GetPrice(ItemType itemType);
        
        public ItemPriceData GetData(ItemType itemType);

        public void SellItem(ItemType itemType);

        public void SellItem(ItemType itemType, int amount);
    }
}