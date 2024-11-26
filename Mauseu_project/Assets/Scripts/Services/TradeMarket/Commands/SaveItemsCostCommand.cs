using Services.TradeMarket.Data;
using Shared.DataProvider.Commands;

namespace Services.TradeMarket.Commands
{
    public class SaveItemsCostCommand : SaveDataCommand<ItemPriceData[]>
    {
        public SaveItemsCostCommand(ItemPriceData[] data) : base(data)
        {
        }
        
        protected override string GetContainerName() => "ItemsCost.json";
    }
}