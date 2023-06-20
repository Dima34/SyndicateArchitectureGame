using System.Collections.Generic;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace Infrastructure.States
{
    public interface IUnearnedLootService : IService, ISavedProgress
    {
        void RemoveIfExists(string id);
        void Add(string id, Vector3 position, Loot loot);
        List<LootPieceData> GetAll();
        bool Contains(string id);
    }
}