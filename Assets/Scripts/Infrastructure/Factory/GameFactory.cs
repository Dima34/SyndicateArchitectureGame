using System;
using Enemy;
using Hero;
using Infrastructure.AssetManagement;
using Infrastructure.Data;
using Infrastructure.Services.PersistantProgress;
using Infrastructure.Services.ProgressDescription;
using Infrastructure.Services.RandomService;
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
        private IInputService _inputService;
        private IRandomService _randomService;
        private readonly IPersistantProgressService _persistantProgressService;
        private IProgressDescriptionService _progressDescriptionService;

        public GameObject HeroGameObject => _heroGameObject;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData, IInputService inputService,
            IRandomService randomService, IPersistantProgressService progressService, IProgressDescriptionService progressDescriptionService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _inputService = inputService;
            _randomService = randomService;
            _persistantProgressService = progressService;
            _progressDescriptionService = progressDescriptionService;
        }

        public GameObject CreateHero(Vector3 instantiatePosition) { 
            _heroGameObject = InstantiateResourceAndRegisterDataUsers(AssetPath.HERO_PATH, instantiatePosition);
            
            SetupHeroAttack();

            return _heroGameObject;
        }

        private void SetupHeroAttack()
        {
            HeroAttack heroAttack = _heroGameObject.GetComponent<HeroAttack>();
            heroAttack.Construct(_inputService);
        }

        public GameObject CreateHUD()
        {
            var hud = InstantiateResourceAndRegisterDataUsers(AssetPath.HUD_PATH);

            ConstructLootCounter(hud);
            
            return hud;
        }

        private void ConstructLootCounter(GameObject hud) =>
            hud.GetComponentInChildren<LootCounter>().Construct(_persistantProgressService.Progress.WorldData);

        public GameObject InstantiateMonster(MonsterTypeId typeId, Transform parent)
        {
            MonsterStaticData _monsterData = _staticData.ForMonster(typeId);
            GameObject monster = Object.Instantiate(_monsterData.Prefab, parent.position, Quaternion.identity, parent);

            SetupMonsterHealth(monster, _monsterData);
            SetupMonsterMovement(monster, _monsterData, HeroGameObject.transform);
            SetupMonsterAttack(monster, _monsterData);
            SetupMonsterLoot(monster, _monsterData);
            
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

        private void SetupMonsterLoot(GameObject monster, MonsterStaticData monsterData)
        {
            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);
            lootSpawner.Construct(this, _randomService);
        }

        public LootPiece CreateLoot()
        {
            var lootPiece = InstantiateResourceAndRegisterDataUsers(Constants.LOOT_RESOURCE_PATH)
                .GetComponent<LootPiece>();
            
            lootPiece.Construct(this, _persistantProgressService.Progress.WorldData);
            
            return lootPiece;
        }

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
        
        public void UnRegisterDataUsers(GameObject gameObject)
        {
            UnRegisterDataReaders(gameObject);
            UnRegisterDataWriters(gameObject);
        }

        private void RegisterDataReaders(GameObject gameObject) => 
            FindComponentsAndExecuteAddCommand<ISavedProgressReader>(gameObject, _progressDescriptionService.RegisterDataReader);

        private void RegisterDataWriters(GameObject gameObject) => 
            FindComponentsAndExecuteAddCommand<ISavedProgressWriter>(gameObject, _progressDescriptionService.RegisterDataWriter);
        
        private void UnRegisterDataReaders(GameObject gameObject) => 
            FindComponentsAndExecuteAddCommand<ISavedProgressReader>(gameObject, _progressDescriptionService.UnRegisterDataReader);

        private void UnRegisterDataWriters(GameObject gameObject) => 
            FindComponentsAndExecuteAddCommand<ISavedProgressWriter>(gameObject, _progressDescriptionService.UnRegisterDataWriter);

        private void FindComponentsAndExecuteAddCommand<MemberType>(GameObject gameObject,Action<MemberType> Add)
        {
            var members = gameObject.GetComponentsInChildren<MemberType>();
            for (int i = 0; i < members.Length; i++)
                Add(members[i]);
        }
    }
}