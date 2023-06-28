using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class WorldData
    {
        [SerializeField] private List<LevelData> _levelsData;
        [SerializeField] private string _currentLevel;
        
        public WorldData()
        {
            _levelsData = new List<LevelData>();
        }

        public string CurrentLevel
        {
            get => _currentLevel;
            set => _currentLevel = value;
        }

        public List<LevelData> LevelsData
        {
            get => _levelsData;
            set => _levelsData = value;
        }
    }
}