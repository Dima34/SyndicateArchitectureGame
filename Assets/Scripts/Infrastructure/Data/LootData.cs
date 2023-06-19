using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class LootData
    {
        [SerializeField] private int _pointsCollected;
        
        public event Action OnChanged;

        public int PointsCollected
        {
            get => _pointsCollected;
            set
            {
                _pointsCollected = value;
                OnChanged?.Invoke();
            }
        }

        public void Collect(Loot loot)
        {
            _pointsCollected += loot.Value;
            OnChanged?.Invoke();
        }
    }
}