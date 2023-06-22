using System;
using UnityEngine;

namespace Logic
{
    public abstract class HealthBase : MonoBehaviour, IHealth
    {
        public abstract float CurrentHP { get; set; }
        public abstract float MaxHP { get; set; }
        public event Action OnHealthChanged;

        public virtual void TakeDamage(float damage)
        {
            OnTakeDamage(damage);
            FireHealthChangeEvent();
        }

        public void FireHealthChangeEvent() =>
            OnHealthChanged?.Invoke();

        public abstract void OnTakeDamage(float damage);
    }
}