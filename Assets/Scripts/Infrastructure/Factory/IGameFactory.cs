using Enemy;
using Infrastructure.Services;
using StaticData;
using UnityEngine;
using Logger = Common.Logger;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero();
        GameObject CreateHUD();
        GameObject InstantiateMonster(MonsterTypeId typeId, Transform parent);
        LootPiece CreateLoot();
        void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);
        Logger Logger { get; }
        void CreateLogger();
    }
}