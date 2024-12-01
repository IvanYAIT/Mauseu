using Services.Inventory.Data;
using Services.Inventory.Items;
using Shared.DataProvider.Commands;

namespace Services.Inventory.Commands
{
    public class LoadInventoryDataCommand : LoadDataCommand<InventoryData>
    {
        protected override InventoryData GetDefault() => new();

        protected override string GetContainerName() => "Inventory.json";
    }
}