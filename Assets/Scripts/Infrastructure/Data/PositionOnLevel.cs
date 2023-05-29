using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public Vector3Data Position;
        public string LevelName;

        public PositionOnLevel(string levelName, Vector3Data position)
        {
            Position = position;
            LevelName = levelName;
        }

        public PositionOnLevel(string initialLevel)
        {
            LevelName = initialLevel;
        }
    }
}