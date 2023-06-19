using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class LootPieceData
    {
        public string Id;
        public Vector3Data Position;
        public Loot Loot;
    }
}