using Services.Inventory.Data;
using Services.Inventory.Items;
using Shared.DataProvider.Commands;

namespace Services.Inventory.Commands
{
    public class LoadInventoryDataCommand : LoadDataCommand<InventoryData>
    {
        protected override InventoryData GetDefault()
        {
            var stackableItems = new StackableItems
            {
                { ItemType.TestResource1, 20 },
                { ItemType.TestResource2, 20 }
            };

            var inventoryData = new InventoryData
            {
                StackableItems = stackableItems,
                UniqItems = new UniqItems()
            };
            
            return inventoryData;
        }

        protected override string GetContainerName() => "Inventory.json";
    }
}