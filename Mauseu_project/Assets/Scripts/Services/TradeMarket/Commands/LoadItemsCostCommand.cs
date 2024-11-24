using Services.TradeMarket.Data;
using Shared.DataProvider.Commands;

namespace Services.TradeMarket.Commands
{
    public class LoadItemsCostCommand : LoadDataCommand<ItemsPriceData>
    {
        protected override string GetContainerName() => "ItemsCost.json";
    }
}