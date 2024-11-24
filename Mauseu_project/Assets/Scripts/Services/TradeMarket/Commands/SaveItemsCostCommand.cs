using Services.TradeMarket.Data;
using Shared.DataProvider.Commands;

namespace Services.TradeMarket.Commands
{
    public class SaveItemsCostCommand : SaveDataCommand<ItemsPriceData>
    {
        public SaveItemsCostCommand(ItemsPriceData data) : base(data)
        {
        }
        
        protected override string GetContainerName() => "ItemsCost.json";
    }
}