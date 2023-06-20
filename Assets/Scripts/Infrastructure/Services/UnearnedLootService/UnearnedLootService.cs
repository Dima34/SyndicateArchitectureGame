using System.Collections.Generic;
using Infrastructure.Data;
using UnityEngine;

namespace Infrastructure.States
{
    public class UnearnedLootService : IUnearnedLootService
    {
        private List<LootPieceData> _unearnedLoot;

        public void LoadProgress(PlayerProgress progress) =>
            _unearnedLoot = progress.WorldData.UnEarnedLootpieces.LootPieces;

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.UnEarnedLootpieces.LootPieces = _unearnedLoot;

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