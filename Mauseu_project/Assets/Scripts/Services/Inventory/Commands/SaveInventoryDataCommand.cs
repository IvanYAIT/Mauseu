using Services.Inventory.Items;
using Shared.DataProvider.Commands;

namespace Services.Inventory.Commands
{
    public class SaveInventoryDataCommand : SaveDataCommand<InventoryData>
    {
        public SaveInventoryDataCommand(InventoryData data) : base(data)
        {
        }

        protected override string GetContainerName() => "Inventory.json";
    }
}