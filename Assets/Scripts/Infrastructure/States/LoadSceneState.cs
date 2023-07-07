using System.Collections.Generic;
using System.Linq;
using Enemy;
using Hero;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.ProgressDescription;
using Infrastructure.Services.StaticData;
using Logic;
using StaticData;
using UI.Elements;
using UI.Services.Factory;
using UI.Services.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressDescriptionService _progressDescriptionService;
        private readonly IUnearnedLootService _unearnedLootService;
        private readonly IStaticDataService _staticData;
        private readonly IWindowService _windowsService;
        private readonly IUIFactory _uiFactory;

        public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IProgressDescriptionService progressDescriptionService, IStaticDataService staticData, IWindowService windowsService, IUIFactory uiFactory, IUnearnedLootService unearnedLootService)
        {
            _staticData = staticData;
            _windowsService = windowsService;
            _uiFactory = uiFactory;
            _unearnedLootService = unearnedLootService;
            _progressDescriptionService = progressDescriptionService;
            _loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string payload)
        {
            _loadingCurtain.Show();
            _progressDescriptionService.CleanupProgressDataUsersList();
            RegisterServicesAsDataUsers();
            
            _sceneLoader.Load(payload, OnLevelLoaded);
        }

        private void OnLevelLoaded()
        {
            InitGameWorld();

            _progressDescriptionService.InformProgressDataReaders();
            CreateUnearnedLoot();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void RegisterServicesAsDataUsers()
        {
            RegisterUnearnedLootService();
        }
        
        private void RegisterUnearnedLootService()
        {
            _progressDescriptionService.RegisterDataReader(_unearnedLootService);
            _progressDescriptionService.RegisterDataWriter(_unearnedLootService);
        }

        private void InitGameWorld()
        {
            CreateLogger();
            
            InitUIRoot();
            InitUIConsole();

            var levelData = LevelStaticData();
            InitSpawners(levelData);
            
            GameObject hero = CreatePlayer();
            CameraFollow(hero);
            CreateHUD(hero);
        }

        private void CreateLogger() =>
            _gameFactory.CreateLogger();

        private void InitUIRoot() =>
            _uiFactory.CreatUIRoot();

        private void InitUIConsole() =>
            _uiFactory.CreateUIConsole();

        private LevelStaticData LevelStaticData()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            return _staticData.ForLevel(currentSceneName);
        }

        private void InitSpawners(LevelStaticData levelStaticData)
        {
            foreach (var spawnerData in levelStaticData.EnemySpawners)
                _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.ID, spawnerData.MonsterType);
        }

        private GameObject CreatePlayer() =>
            _gameFactory.CreateHero();

        private static void CameraFollow(GameObject player) =>
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(player);

        private void CreateHUD(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHUD();
            
            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(_windowsService);

            HeroHealth heroHealth = hero.GetComponent<HeroHealth>();
            hud.GetComponentInChildren<ActorUI>().Construct(heroHealth);
        }

        private void CreateUnearnedLoot()
        {
            List<LootPieceData> unearnedLootList = _unearnedLootService.GetAll();
            
            List<LootPieceData> ListToIterate = unearnedLootList.ToList();
            unearnedLootList.Clear();          

            // Copy the list because inside of cycle we add elements inside list which we iterate
            foreach (var lootItem in ListToIterate.ToList())
            {
                LootPiece loot = _gameFactory.CreateLoot();
                loot.Initialize(lootItem.Loot, lootItem.Position.AsUnityVector());
            }
        }

        public void Exit() =>
            _loadingCurtain.Hide();
    }
}