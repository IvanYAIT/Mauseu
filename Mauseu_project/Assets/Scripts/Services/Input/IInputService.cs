using UnityEngine;

namespace Services.Input
{
    public interface IInputService
    {
        float Horizontal { get; }
        float Vertical { get; }
        
        Vector2 Axis { get; }
        bool IsAttackButtonUp();

        void SetActiveCursor(bool isActive);
        void SetActiveMobility(bool isActive);
    }
}