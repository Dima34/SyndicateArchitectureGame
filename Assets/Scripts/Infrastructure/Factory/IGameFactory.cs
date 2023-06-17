using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.PersistantProgress;
using StaticData;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgressWriter> ProgressWriters { get; }

        GameObject CreateHero(Vector3 instantiatePosition);
        GameObject CreateHUD();
        void CleanupProgressMembersList();
        public void RegisterDataUsers(GameObject gameObject);
        GameObject InstantiateMonster(MonsterTypeId typeId, Transform parent);
        GameObject CreateLoot();
    }
}