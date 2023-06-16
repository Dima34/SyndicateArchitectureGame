using System;
using System.Collections.Generic;
using Enemy;
using Hero;
using Infrastructure.AssetManagement;
using Infrastructure.Services;
using Infrastructure.Services.PersistantProgress;
using Logic;
using Services.Inputs;
using StaticData;
using UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private GameObject _heroGameObject;
        private IStaticDataService _staticData;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriters { get; } = new List<ISavedProgressWriter>();
        public GameObject HeroGameObject => _heroGameObject;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
        }

        public GameObject CreateHero(Vector3 instantiatePosition) { 
            _heroGameObject = InstantiateResourceAndRegisterDataUsers(AssetPath.HERO_PATH, instantiatePosition);
            
            SetupHeroAttack();

            return _heroGameObject;
        }

        private void SetupHeroAttack()
        {
            HeroAttack heroAttack = _heroGameObject.GetComponent<HeroAttack>();
            heroAttack.Construct(AllServices.Container.GetSingle<IInputService>());
        }

        public GameObject CreateHUD() =>
            InstantiateResourceAndRegisterDataUsers(AssetPath.HUD_PATH);

        private GameObject InstantiateResourceAndRegisterDataUsers(string assetPath, Vector3 at)
        {
            var gameObject = _assetProvider.InstantiateResourse(assetPath, at);
            RegisterDataUsers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateResourceAndRegisterDataUsers(string assetPath)
        {
            var gameObject = _assetProvider.InstantiateResourse(assetPath);
            RegisterDataUsers(gameObject);
            return gameObject;
        }

        public void RegisterDataUsers(GameObject gameObject)
        {
            RegisterDataReaders(gameObject);
            RegisterDataWriters(gameObject);
        }

        public GameObject InstantiateMonster(MonsterTypeId typeId, Transform parent)
        {
            MonsterStaticData _monsterData = _staticData.ForMonster(typeId);
            GameObject monster = Object.Instantiate(_monsterData.Prefab, parent.position, Quaternion.identity, parent);

            SetupMonsterHealth(monster, _monsterData);
            SetupMonsterMovement(monster, _monsterData, HeroGameObject.transform);
            SetupMonsterAttack(monster, _monsterData);
            
            return monster;
        }

        private static void SetupMonsterHealth(GameObject monster, MonsterStaticData monsterData)
        {
            var monsterHealth = monster.GetComponent<IHealth>();
            monsterHealth.CurrentHP = monsterData.Hp;
            monsterHealth.MaxHP = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(monsterHealth);
        }

        private static void SetupMonsterMovement(GameObject monster, MonsterStaticData monsterData, Transform heroTransform)
        {
            monster.GetComponent<NavMeshAgent>().speed = monsterData.Speed;
            monster.GetComponent<AgentMoveToHero>()?.Construct(heroTransform);
            monster.GetComponent<AgentRotateToHero>()?.Construct(heroTransform);
        }

        private void SetupMonsterAttack(GameObject monster, MonsterStaticData monsterData)
        {
            var monsterAttack = monster.GetComponent<Attack>();
            monsterAttack.Construct(HeroGameObject.transform);
            monsterAttack.Damage = monsterData.Damage;
            monsterAttack.Cleavage = monsterData.Cleverage;
            monsterAttack.EffectiveDistance = monsterData.EffectiveDistance;
            monsterAttack.AttackCooldown = monsterData.AttackCooldown;
        }

        private void RegisterDataReaders(GameObject gameObject) => 
            FindComponentsAndAddToList<ISavedProgressReader>(gameObject, ProgressReaders);

        private void RegisterDataWriters(GameObject gameObject) => 
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