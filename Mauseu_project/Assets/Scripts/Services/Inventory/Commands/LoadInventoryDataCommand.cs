using Services.Inventory.Data;
using Shared.DataProvider.Commands;

namespace Services.Inventory.Commands
{
    public class LoadInventoryDataCommand : LoadDataCommand<InventoryData>
    {
        protected override InventoryData GetDefault()
        {
            var inventoryData = new InventoryData
            {
                StackableItems = new StackableItems(),
                UniqItems = new UniqItems()
            };
            
            return inventoryData;
        }

        protected override string GetContainerName() => "Inventory.json";
    }
}