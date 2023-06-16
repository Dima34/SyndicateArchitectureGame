using System;
using Logic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : HealthBase
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;
        
        private float _current;
        private float _max;
        
        public override float CurrentHP
        {
            get => _current;
            set => _current = value;
        }
        
        public override float MaxHP
        {
            get => _max;
            set => _max = value;
        }

        public override void OnTakeDamage(float damage)
        {
            _current -= damage;
            _enemyAnimator.PlayHit();
        }
    }
}