using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel
        {
            get => _positionOnLevel;
            set => _positionOnLevel = value;
        }
        
        [SerializeField]
        private PositionOnLevel _positionOnLevel;

        public WorldData()
        {
            _positionOnLevel = new PositionOnLevel();
        }
    }
}