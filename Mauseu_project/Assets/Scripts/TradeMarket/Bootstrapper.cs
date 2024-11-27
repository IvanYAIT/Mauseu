using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.ServiceLocator;
using Dependencies.ChaserLib.Tasks;
using Services.Inventory;
using Services.Inventory.Commands;
using Services.TradeMarket;
using Services.TradeMarket.Commands;
using Services.TradeMarket.Data;
using Services.Wallet;
using UnityEngine;

namespace TradeMarket
{
    [AddComponentMenu("Game/TradeMarket Bootstrapper")]
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private DialogsLauncher _dialogsLauncher;
        [SerializeField] private DefaultItemsCostConfig _defaultItemsCostConfig;

        private static ServiceLocator Locator => ServiceLocator.Instance;

        public void Start()
        {
            var tokenFactory = new CancellationTokenFactory(destroyCancellationToken);
            var inventoryService = LoadInventory();
            var walletService = LoadWallet();
            var tradeService = LoadTradeService();

            Locator.Add<IDialogsLauncher>(_dialogsLauncher);
            Locator.Add<ICancellationTokenFactory>(tokenFactory);
            Locator.Add(inventoryService);
            Locator.Add(walletService);
            Locator.Add(tradeService);
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
    }
}