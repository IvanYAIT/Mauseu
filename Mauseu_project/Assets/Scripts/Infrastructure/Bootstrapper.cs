using UnityEngine;

namespace Infrastructure
{
    [AddComponentMenu("Game/Game Bootstrapper")]
    public class Bootstrapper : MonoBehaviour
    {
        //[SerializeField] private DialogsLauncher _dialogsLauncher;
        [SerializeField] private Character.CharacterController _characterController;
        
        private Game _game;

        public void Awake()
        {
            _game = new Game(_characterController);

            DontDestroyOnLoad(this);

            // await UniTask.Delay(TimeSpan.FromSeconds(1f));
            //
            // _dispatcher = new EventDispatcher();
            // Locator.Add(_dispatcher);
            // Locator.Add<IDialogsLauncher>(_dialogsLauncher);
            //
            // var token = gameObject.GetCancellationTokenOnDestroy();
            // Locator.Add<ICancellationTokenFactory>(new CancellationTokenFactory(token));
            //
            // BindHandlers();
        }

        private void BindHandlers()
        {
        }
    }
}