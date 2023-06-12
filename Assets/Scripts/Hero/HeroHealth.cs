using System;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace Player
{
    public class HeroHealth : MonoBehaviour, ISavedProgress
    {
        public float Current
        {
            get => _state.CurrentHp;
            set
            {
                if (value < 0)
                    _state.CurrentHp = Constants.HERO_MINIMAL_HP;
                else
                    _state.CurrentHp = value;

                OnHealthChanged?.Invoke();
            }
        }

        public float Max
        {
            get => _state.MaxHp;
            set => _state.MaxHp = value;
        }

        private State _state;
        public Action OnHealthChanged;

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            OnHealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHp = Current;
            progress.HeroState.MaxHp = Max;
            
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
        }
    }
}