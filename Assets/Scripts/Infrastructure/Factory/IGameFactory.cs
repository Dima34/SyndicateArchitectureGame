using System.Threading.Tasks;
using Enemy;
using Infrastructure.Services;
using StaticData;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        Task<GameObject> CreateHero();
        Task<GameObject> CreateHUD();
        Task<GameObject> InstantiateMonster(MonsterTypeId typeId, Transform parent);
        Task<LootPiece> CreateLoot();
        Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);
        void CreateLogger();
        void CleanUp();
        void WarmUp();
    }
}