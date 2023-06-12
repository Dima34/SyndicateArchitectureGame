using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class PlayerProgress
    {
        [SerializeField] private WorldData _worldData;
        [SerializeField] private State _heroState;

        public WorldData WorldData => _worldData;

        public State HeroState
        {
            get => _heroState;
            set => _heroState = value;
        }


        public PlayerProgress()
        {
            _worldData = new WorldData();
            _heroState = new State();
        }
    }
}