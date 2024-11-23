using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.ServiceLocator;
using Dependencies.ChaserLib.Tasks;
using UnityEngine;

[AddComponentMenu("Game/TradeMarket Bootstrapper")]
public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private DialogsLauncher _dialogsLauncher;
    
    private static ServiceLocator Locator => ServiceLocator.Instance;
    
    public void Start()
    {
        var tokenFactory = new CancellationTokenFactory(destroyCancellationToken);
        
        Locator.Add<IDialogsLauncher>(_dialogsLauncher);
        Locator.Add<ICancellationTokenFactory>(tokenFactory);
    }
}
