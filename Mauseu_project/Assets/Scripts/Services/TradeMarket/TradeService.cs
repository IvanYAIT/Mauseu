using System;
using System.Linq;
using Dependencies.ChaserLib.ServiceLocator;
using Services.Inventory;
using Services.Inventory.Items;
using Services.TradeMarket.Commands;
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

        public ItemPriceData GetData(ItemType itemType) => _itemsPrices.GetPriceData(itemType);

        public void SellItem(ItemType itemType) => SellItem(itemType, 1);

        public void SellItem(ItemType itemType, int amount)
        {
            var data = GetData(itemType);
            var cost = data.Cost;

            WalletService.Add(cost * amount);

            if (data.Category is not (ItemCategory.Weapon or ItemCategory.Monsters))
            {
                InventoryService.RemoveItem(itemType, amount);
            }

            if (!data.HasFlexiblePrice)
                return;

            FlexPrice(data.Category, itemType);
        }

        public void SellItem(Guid id)
        {
            var item = InventoryService.GetAllUniqItems().First(i => i.Id == id);
            var itemType = item.Type;
            
            SellItem(itemType);
            InventoryService.RemoveItem(id);
        }

        private void FlexPrice(ItemCategory category, ItemType type)
        {
            var data = GetData(type);
            var priceDelta = TradeConstants.CostChangeModifier / data.Valuability;

            _itemsPrices.ModifyCost(type, data.Cost - priceDelta);

            var itemsToChange = _itemsPrices.GetItemsInCategory(category, type);
            var totalValuability = itemsToChange.Sum(i => i.Valuability);

            foreach (var item in itemsToChange)
            {
                var additionalPrice = (float) item.Valuability / totalValuability * priceDelta;
                _itemsPrices.ModifyCost(item.Type, item.Cost + (int) additionalPrice);
            }

            new SaveItemsCostCommand(_itemsPrices.GetAllItems()).Execute();
        }
    }
}