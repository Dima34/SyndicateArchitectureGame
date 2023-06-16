using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class HeroStats
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _hitRadius;
        [SerializeField] private float _hitForwardOffset;
        
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
    }
}