using System;
using Logic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(IHealth))]
    public abstract class MortalBase : MonoBehaviour
    {
        [SerializeField] internal HealthBase _healthToTrack;
        
        public event Action Happend;

        private void Start() =>
            StartTrackHealth();

        private void OnDestroy() =>
            EndTrackHealth();

        private void StartTrackHealth() =>
            _healthToTrack.OnHealthChanged += OnHealthToTrackChanged;

        private void EndTrackHealth() =>
            _healthToTrack.OnHealthChanged -= OnHealthToTrackChanged;

        private void OnHealthToTrackChanged()
        {
            if (IsDead())
            {
                Die();
                Happend?.Invoke();
                EndTrackHealth();
            }
        }

        protected abstract bool IsDead();
        protected abstract void Die();
    }
}