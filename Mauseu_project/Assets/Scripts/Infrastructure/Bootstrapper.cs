using Dependencies.ChaserLib.ServiceLocator;
using Services.Input;
using UnityEngine;

namespace Infrastructure
{
    [AddComponentMenu("Game/Game Bootstrapper")]
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Services.Character.CharacterController _characterController;
        
        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IInputService InputService => Locator.Get<IInputService>();
        
        public void Awake()
        {
            _characterController.Init(InputService);
            
            DontDestroyOnLoad(this);
        }
    }
}