namespace Infrastructure.Data
{
    public class PositionOnLevel
    {
        public string LevelName { get; }
        public Vector3Data Position { get; }

        public PositionOnLevel(string levelName, Vector3Data position)
        {
            LevelName = levelName;
            Position = position;
        }

        public PositionOnLevel(string initialLevel)
        {
            LevelName = initialLevel;
        }
    }
}