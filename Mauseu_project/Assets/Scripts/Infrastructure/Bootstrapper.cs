using UnityEngine;

namespace Infrastructure
{
    [AddComponentMenu("Game/Game Bootstrapper")]
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Services.Character.CharacterController _characterController;
        
        private Game _game;

        public void Awake()
        {
            _game = new Game(_characterController);

            DontDestroyOnLoad(this);
        }

        private void BindHandlers()
        {
        }
    }
}