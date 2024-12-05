using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.Dialogs.Events;
using Dependencies.ChaserLib.ServiceLocator;
using Plugins.EventHandler;
using Plugins.EventHandler.Handlers;

namespace Services.Input.Handlers
{
    public class CursorHandler : EventHandler
    {
        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IInputService InputService => Locator.Get<IInputService>();
        private static IDialogsLauncher DialogsLauncher => Locator.Get<IDialogsLauncher>();

        public CursorHandler(IEvent ev) : base(ev)
        {
        }

        public override void Handle()
        {
            var isActive = EventIs<ShowDialogEvent>();

            if (isActive)
            {
                InputService.SetActiveCursor(true);
                InputService.SetActiveMobility(false);
            }
            else
            {
                if (DialogsLauncher.HasOpenDialog())
                    return;

                InputService.SetActiveCursor(false);
                InputService.SetActiveMobility(true);
            }
        }
    }
}