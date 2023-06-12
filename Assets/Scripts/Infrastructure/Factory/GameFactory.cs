using System;
using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Infrastructure.Services;
using Infrastructure.Services.PersistantProgress;
using Player;
using Services.Inputs;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private GameObject _heroGameObject;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriters { get; } = new List<ISavedProgressWriter>();
        public GameObject HeroGameObject => _heroGameObject;
        public event Action OnHeroCreated;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateHero(Vector3 instantiatePosition) { 
            _heroGameObject = InstantiateResourceAndRegisterDataUsers(AssetPath.HERO_PATH, instantiatePosition);
            HeroAttack heroAttack = _heroGameObject.GetComponent<HeroAttack>();
            heroAttack.Construct(AllServices.Container.GetSingle<IInputService>());
            OnHeroCreated?.Invoke();
            
            return _heroGameObject;
        } 

        public GameObject CreateHUD() =>
            InstantiateResourceAndRegisterDataUsers(AssetPath.HUD_PATH);

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