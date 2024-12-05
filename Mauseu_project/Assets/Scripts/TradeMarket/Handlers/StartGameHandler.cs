using Dependencies.ChaserLib.ServiceLocator;
using JetBrains.Annotations;
using Plugins.EventHandler;
using Plugins.EventHandler.Handlers;
using Services.Input;

namespace TradeMarket.Handlers
{
    [UsedImplicitly]
    internal class StartGameHandler : EventHandler
    {
        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IInputService InputService => Locator.Get<IInputService>();
        
        public StartGameHandler(IEvent ev) : base(ev)
        {
        }

        public override void Handle()
        {
            InputService.SetActiveCursor(false);
            InputService.SetActiveMobility(true);
        }
    }
}