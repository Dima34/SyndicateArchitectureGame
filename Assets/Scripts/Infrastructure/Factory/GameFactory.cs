using System;
using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Infrastructure.Services.PersistantProgress;
using Unity.VisualScripting;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriters { get; } = new List<ISavedProgressWriter>();

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateHero(Vector3 instantiatePosition) => 
            InstantiateResourceAndRegisterDataUsers(AssetPath.HERO_PATH, instantiatePosition);

        public void CreateHUD()
        {
            InstantiateResourceAndRegisterDataUsers(AssetPath.HUD_PATH);
        }

        private GameObject InstantiateResourceAndRegisterDataUsers(string assetPath, Vector3 at)
        {
            var gameObject = _assetProvider.InstantiateResourse(assetPath, at);
            RegisterHeroDataUsers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateResourceAndRegisterDataUsers(string assetPath)
        {
            var gameObject = _assetProvider.InstantiateResourse(assetPath);
            RegisterHeroDataUsers(gameObject);
            return gameObject;
        }

        private void RegisterHeroDataUsers(GameObject gameObject)
        {
            RegisterProgressReaders(gameObject);
            RegisterProgressWriters(gameObject);
        }

        private void RegisterProgressReaders(GameObject gameObject) => 
            FindComponentsAndAddToList<ISavedProgressReader>(gameObject, ProgressReaders);

        private void RegisterProgressWriters(GameObject gameObject) => 
            FindComponentsAndAddToList<ISavedProgressWriter>(gameObject, ProgressWriters);

        private void FindComponentsAndAddToList<MemberType>(GameObject gameObject,List<MemberType> membersList)
        {
            var memebers = gameObject.GetComponentsInChildren<MemberType>();
            for (int i = 0; i < memebers.Length; i++)
                membersList.Add(memebers[i]);
        }

        public void CleanupProgressMembersList()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}