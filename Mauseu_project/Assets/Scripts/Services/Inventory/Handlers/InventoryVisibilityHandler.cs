using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.ServiceLocator;
using Plugins.EventHandler;
using Plugins.EventHandler.Handlers;
using Services.Inventory.View;

namespace Services.Inventory.Handlers
{
    public class InventoryVisibilityHandler : EventHandler
    {
        private static ServiceLocator ServiceLocator => ServiceLocator.Instance;
        private static IDialogsLauncher DialogsLauncher => ServiceLocator.Get<IDialogsLauncher>();

        public InventoryVisibilityHandler(IEvent ev) : base(ev)
        {
        }

        public override void Handle()
        {
            var dialog = DialogsLauncher.Show<InventoryDialog>(DialogType.Inventory);
        }
    }
}