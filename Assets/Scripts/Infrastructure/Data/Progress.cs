using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class Progress
    {
        [SerializeField] private WorldData _worldData;
        [SerializeField] private HeroStats _heroStats;
        [SerializeField] private KillData _killData;
        [SerializeField] private CollectedPointsData collectedCollectedPointsData;

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
        
        public CollectedPointsData CollectedPointsData
        {
            get => collectedCollectedPointsData;
            set => collectedCollectedPointsData = value;
        }

        public Progress()
        {
            _worldData = new WorldData();
            _killData = new KillData();
            _heroStats = new HeroStats();
            collectedCollectedPointsData = new CollectedPointsData();
        }
    }
}