using System.Collections.Generic;
using Infrastructure.Data;
using UnityEngine;

namespace Infrastructure.States
{
    public class UnearnedLootService : IUnearnedLootService
    {
        private List<LootPieceData> _unearnedLoot = new List<LootPieceData>();

        public void LoadProgress(Progress progress) =>
            _unearnedLoot = progress.WorldData.GetCurrentLevelData().UnEarnedLootPieces;
    
        public void UpdateProgress(Progress progress) =>
            progress.WorldData.GetCurrentLevelData().UnEarnedLootPieces = _unearnedLoot;

        public void RemoveIfExists(string id)
        {
            var foundIndex = _unearnedLoot.FindIndex(x => x.Id == id);

            if (foundIndex >= 0) 
                _unearnedLoot.RemoveAt(foundIndex);
        }

        public void Add(string id, Vector3 position, Loot loot)
        {
            _unearnedLoot.Add(new ()
            {
                Id = id,
                Loot = loot,
                Position = position.AsVectorData()
            });
        }

        public bool Contains(string id) =>
            _unearnedLoot.Exists(x => x.Id == id);

        public List<LootPieceData> GetAll() =>
            _unearnedLoot;
    }
}