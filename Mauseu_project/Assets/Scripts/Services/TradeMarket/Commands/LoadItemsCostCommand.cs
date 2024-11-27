using Services.TradeMarket.Data;
using Shared.DataProvider.Commands;

namespace Services.TradeMarket.Commands
{
    public class LoadItemsCostCommand : LoadDataCommand<ItemPriceData[]>
    {
        private readonly ItemPriceData[] _itemPriceDatas;

        public LoadItemsCostCommand(ItemPriceData[] itemPriceDatas) => _itemPriceDatas = itemPriceDatas;

        protected override ItemPriceData[] GetDefault() => _itemPriceDatas;

        protected override string GetContainerName() => "ItemsCost.json";
    }
}