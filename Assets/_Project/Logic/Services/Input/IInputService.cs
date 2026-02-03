using UnityEngine;

namespace _Project.Logic.Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }

        bool IsAttackButtonUp();
    }
}