using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel { get; set; }

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
        }
    }
}