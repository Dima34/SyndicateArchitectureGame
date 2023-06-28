using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class CollectedPointsData
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
        public void Add(int value)
        {
            _pointsCollected += value;
            OnChanged?.Invoke();
        }
    }
}