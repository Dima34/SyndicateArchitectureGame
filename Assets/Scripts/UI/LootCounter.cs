using System;
using Infrastructure.Data;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LootCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        private WorldData _worldData;

        private void Start() =>
            UpdateCounter();

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.OnChanged += UpdateCounter;
        }

        private void UpdateCounter() =>
            _counter.text = $"{_worldData.LootData.PointsCollected}";
    }
}