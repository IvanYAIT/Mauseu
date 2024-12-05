using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.Dialogs.Events;
using Dependencies.ChaserLib.ServiceLocator;
using Dependencies.ChaserLib.Tasks;
using Plugins.EventDispatching.Dispatcher;
using Services.Forge;
using Services.Input;
using Services.Input.Handlers;
using Services.Inventory;
using Services.Inventory.Commands;
using Services.TradeMarket;
using Services.TradeMarket.Commands;
using Services.TradeMarket.Data;
using Services.Wallet;
using Services.Weapons;
using Services.Weapons.Commands;
using Services.Weapons.Data;
using TradeMarket.Events;
using TradeMarket.Handlers;
using UnityEngine;

namespace TradeMarket
{
    [AddComponentMenu("Game/TradeMarket Bootstrapper")]
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private DialogsLauncher _dialogsLauncher;
        [SerializeField] private DefaultItemsCostConfig _defaultItemsCostConfig;
        [SerializeField] private WeaponsData _weaponsData;
        [SerializeField] private Services.Character.CharacterController _characterController;

        private static ServiceLocator Locator => ServiceLocator.Instance;

        private IEventDispatcher _dispatcher;

        public void Start()
        {
            var tokenFactory = new CancellationTokenFactory(destroyCancellationToken);
            var inventoryService = LoadInventory();
            var walletService = LoadWallet();
            var tradeService = LoadTradeService();
            var weaponService = LoadWeaponService();
            var forgeService = LoadForgeService();
            _dispatcher = LoadEventHandler();

            Locator.Add<IDialogsLauncher>(_dialogsLauncher);
            Locator.Add<ICancellationTokenFactory>(tokenFactory);
            Locator.Add(inventoryService);
            Locator.Add(walletService);
            Locator.Add(tradeService);
            Locator.Add(weaponService);
            Locator.Add(forgeService);

            Locator.Add(_dispatcher);


            var inputService = new InputService();
            Locator.Add<IInputService>(inputService);
            _characterController.Init(inputService);

            SetupBindings();

            _dispatcher.Raise(new StartGameEvent());
        }

        private static IInventoryService LoadInventory()
        {
            var data = new LoadInventoryDataCommand().Execute();
            return new InventoryService(data);
        }

        private static IWalletService LoadWallet()
        {
            var data = new LoadWalletCommand().Execute();
            return new WalletService(data);
        }

        private ITradeService LoadTradeService()
        {
            var data = new LoadItemsCostCommand(_defaultItemsCostConfig.GetAllItems()).Execute();
            var itemPricesData = new ItemsPriceData(data);
            return new TradeService(itemPricesData);
        }

        private IWeaponService LoadWeaponService()
        {
            var data = new LoadWeaponDataCommand().Execute();
            return new WeaponService(data, _weaponsData);
        }

        private static IForgeService LoadForgeService()
        {
            return new ForgeService();
        }

        private static IEventDispatcher LoadEventHandler()
        {
            return new EventDispatcher();
        }

        private void SetupBindings()
        {
            _dispatcher.Bind().Handler<StartGameHandler>().To<StartGameEvent>();
            
            _dispatcher.Bind().Handler<CursorHandler>().To<ShowDialogEvent>();
            _dispatcher.Bind().Handler<CursorHandler>().To<HideDialogEvent>();
        }
    }
}