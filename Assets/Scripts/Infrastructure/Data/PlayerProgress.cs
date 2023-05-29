using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData => _worldData;
        [SerializeField]
        private WorldData _worldData;

        public PlayerProgress(string initialLevel)
        {
            _worldData = new WorldData(initialLevel);
        }
    }
}