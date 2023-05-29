using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        void CreateHUD();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgressWriter> ProgressWriters { get; }
        void CleanupProgressMembersList();
    }
}