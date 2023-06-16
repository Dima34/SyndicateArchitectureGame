using System;

namespace Logic
{
    public interface IHealth
    {
        public event Action OnHealthChanged;
        float CurrentHP { get; set; }
        float MaxHP { get; set; }
        void TakeDamage(float damage);
    }
}