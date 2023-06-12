using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class State
    {
        [SerializeField] private float _currentHP;
        [SerializeField] private float _maxHP;
        
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

        public void ResetHP() => _currentHP = _maxHP;
    }
}