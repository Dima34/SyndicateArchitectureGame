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
        private HeroStats _stats;

        public override float CurrentHP
        {
            get => _stats.CurrentHp;
            set
            {
                _stats.CurrentHp = value > 0 ? _stats.CurrentHp = value : 0;

                FireHealthChangeEvent();
            }
        }

        public override float MaxHP
        {
            get => _stats.MaxHp;
            set => _stats.MaxHp = value;
        }

        public override void OnTakeDamage(float damage) =>
            CurrentHP -= damage;

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.HeroStats;
            FireHealthChangeEvent();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroStats.CurrentHp = CurrentHP;
            progress.HeroStats.MaxHp = MaxHP;
        }
    }
}