using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.ServiceLocator;
using Dependencies.ChaserLib.Tasks;
using Services.Inventory;
using Services.Inventory.Data;
using UnityEngine;

namespace TradeMarket
{
    [AddComponentMenu("Game/TradeMarket Bootstrapper")]
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private DialogsLauncher _dialogsLauncher;
    
        private static ServiceLocator Locator => ServiceLocator.Instance;
    
        public void Start()
        {
            var tokenFactory = new CancellationTokenFactory(destroyCancellationToken);
            var inventoryService = LoadInventory();
        
            Locator.Add<IDialogsLauncher>(_dialogsLauncher);
            Locator.Add<ICancellationTokenFactory>(tokenFactory);
            Locator.Add(inventoryService);
        }

        private static IInventoryService LoadInventory()
        {
            var data = new LoadDataCommand().Execute();
            return new InventoryService(data);
        }
    }
}
