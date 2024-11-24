using Dependencies.ChaserLib.ServiceLocator;
using Services.Inventory;
using Services.Inventory.Items;
using Services.TradeMarket.Data;
using Services.Wallet;

namespace Services.TradeMarket
{
    public class TradeService : ITradeService
    {
        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IInventoryService InventoryService => Locator.Get<IInventoryService>();
        private static IWalletService WalletService => Locator.Get<IWalletService>();

        private readonly ItemsPriceData _itemsPrices;

        public TradeService(ItemsPriceData itemsPrices) => _itemsPrices = itemsPrices;

        public int GetPrice(ItemType itemType) => _itemsPrices.GetPrice(itemType);

        public void SellItem(ItemType itemType) => SellItem(itemType, 1);

        public void SellItem(ItemType itemType, int amount)
        {
            var cost = GetPrice(itemType);
            WalletService.Add(cost * amount);
            InventoryService.RemoveItem(itemType, amount);
        }
    }
}