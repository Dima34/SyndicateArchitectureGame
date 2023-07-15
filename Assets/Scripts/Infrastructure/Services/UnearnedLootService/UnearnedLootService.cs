using System.Collections.Generic;
using Infrastructure.Data;
using UnityEngine;

namespace Infrastructure.States
{
    public class UnearnedLootService : IUnearnedLootService
    {
        private List<LootPieceData> _unearnedLoot = new List<LootPieceData>();

        public void UpdateProgress(Progress progress) =>
            progress.WorldData.GetCurrentLevelData().UnEarnedLootPieces = _unearnedLoot;

        public void Add(string id, Vector3 position, Loot loot)
        {
            _unearnedLoot.Add(new ()
            {
                Id = id,
                Loot = loot,
                Position = position.AsVectorData()
            });
        }
    }
}