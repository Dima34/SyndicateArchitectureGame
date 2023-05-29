using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class PlayerProgress
    {
        private WorldData _worldData;
        public WorldData WorldData => _worldData;

        public PlayerProgress(string initialLevel)
        {
            _worldData = new WorldData(initialLevel);
        }
    }
}