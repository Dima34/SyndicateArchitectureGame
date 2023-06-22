using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class HeroStats
    {
        [SerializeField] private float _currentHP;
        [SerializeField] private float _maxHP;
        [SerializeField] private float _damage;
        [SerializeField] private float _hitRadius;
        [SerializeField] private float _hitForwardOffset;
        [SerializeField] private int _hitObjectsPerHit;

        public float CurrentHp
        {
            get => _currentHP;
            set => _currentHP = value;
        }

        public float MaxHp
        {
            get => _maxHP;
            set => _maxHP = value;
        }

        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public float HitRadius
        {
            get => _hitRadius;
            set => _hitRadius = value;
        }

        public float HitForwardOffset
        {
            get => _hitForwardOffset;
            set => _hitForwardOffset = value;
        }

        public int HitObjectPerHit { 
            get => _hitObjectsPerHit;
            set => _hitObjectsPerHit = value;
        }
    }
}