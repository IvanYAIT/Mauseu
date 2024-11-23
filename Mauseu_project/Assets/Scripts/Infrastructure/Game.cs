using Services.Input;

namespace Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

        public Game(Character.CharacterController characterController)
        {
            InputService = new InputService();
            characterController.Init(InputService);
        }
    }
}