using Services.Character;
using Services.Input;

namespace Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

        public Game(CharacterController characterController)
        {
            InputService = new InputService();
            characterController.Init(InputService);
        }
    }
}