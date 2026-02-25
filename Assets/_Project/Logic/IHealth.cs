using System;

namespace _Project.Logic
{
    public interface IHealth
    {
        event Action HealthChanged;

        float Current { get; set; }

        float Max { get; set; }

        void TakeDamage(float damage);
    }
}