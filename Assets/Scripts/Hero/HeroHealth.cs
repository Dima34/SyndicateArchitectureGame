using System;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Services.PersistantProgress;
using Logic;
using UnityEngine;

namespace Hero
{
    public class HeroHealth : HealthBase, ISavedProgress
    {
        public override float CurrentHP
        {
            get => _state.CurrentHp;
            set
            {
                if (value < 0)
                    _state.CurrentHp = Constants.HERO_MINIMAL_HP;
                else
                    _state.CurrentHp = value;

                FireHealthChangeEvent();
            }
        }

        public override float MaxHP
        {
            get => _state.MaxHp;
            set => _state.MaxHp = value;
        }

        private State _state;

        public override void OnTakeDamage(float damage) =>
            CurrentHP -= damage;

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            FireHealthChangeEvent();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHp = CurrentHP;
            progress.HeroState.MaxHp = MaxHP;
        }
    }
}