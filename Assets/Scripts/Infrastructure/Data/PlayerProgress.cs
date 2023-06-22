using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class PlayerProgress
    {
        [SerializeField] private WorldData _worldData;
        [SerializeField] private HeroStats _heroStats;
        [SerializeField] private KillData _killData;

        public WorldData WorldData => _worldData;

        public HeroStats HeroStats
        {
            get => _heroStats;
            set => _heroStats = value;
        }

        public KillData KillData
        {
            get => _killData;
            set => _killData = value;
        }

        public PlayerProgress()
        {
            _worldData = new WorldData();
            _killData = new KillData();
            _heroStats = new HeroStats();
        }
    }
}