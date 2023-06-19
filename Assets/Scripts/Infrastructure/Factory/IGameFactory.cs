using Enemy;
using Infrastructure.Services;
using StaticData;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(Vector3 instantiatePosition);
        GameObject CreateHUD();
        public void RegisterDataUsers(GameObject gameObject);
        GameObject InstantiateMonster(MonsterTypeId typeId, Transform parent);
        LootPiece CreateLoot();
        void UnRegisterDataUsers(GameObject gameObject);
    }
}