using Services.Inventory.Items;
using Shared.DataProvider.Commands;

namespace Services.Inventory.Commands
{
    public class LoadInventoryDataCommand : LoadDataCommand<InventoryData>
    {
        protected override string GetContainerName() => "Inventory.json";
    }
}