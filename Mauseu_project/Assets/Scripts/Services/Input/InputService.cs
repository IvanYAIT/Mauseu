using UnityEngine;

namespace Services.Input
{
    public class InputService : IInputService
    {
        private const string HorizontalKey = "Horizontal";
        private const string VerticalKey = "Vertical";
        private const string AttackKey = "Fire";

        public float Horizontal => UnityEngine.Input.GetAxis(HorizontalKey);
        public float Vertical => UnityEngine.Input.GetAxis(VerticalKey);

        public Vector2 Axis => new(Vertical, Horizontal);

        public bool IsAttackButtonUp() => UnityEngine.Input.GetButtonUp(AttackKey);
    }
}