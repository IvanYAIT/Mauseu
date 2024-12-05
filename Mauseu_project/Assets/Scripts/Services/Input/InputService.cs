using UnityEngine;

namespace Services.Input
{
    public class InputService : IInputService
    {
        private const string HorizontalKey = "Horizontal";
        private const string VerticalKey = "Vertical";
        private const string AttackKey = "Fire";

        public float Horizontal => _canMove ? UnityEngine.Input.GetAxis(HorizontalKey) : 0;
        public float Vertical => _canMove ? UnityEngine.Input.GetAxis(VerticalKey) : 0;

        public Vector2 Axis => new(Vertical, Horizontal);

        private bool _canMove;

        public bool IsAttackButtonUp() => UnityEngine.Input.GetButtonUp(AttackKey);

        public void SetActiveCursor(bool isActive)
        {
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isActive;
        }

        public void SetActiveMobility(bool isActive) => _canMove = isActive;
    }
}