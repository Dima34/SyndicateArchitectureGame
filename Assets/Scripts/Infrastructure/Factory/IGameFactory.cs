using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.PersistantProgress;
using Unity.VisualScripting;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgressWriter> ProgressWriters { get; }
        GameObject HeroGameObject { get; }

        event Action OnHeroCreated;

        GameObject CreateHero(Vector3 instantiatePosition);
        GameObject CreateHUD();
        void CleanupProgressMembersList();
    }
}