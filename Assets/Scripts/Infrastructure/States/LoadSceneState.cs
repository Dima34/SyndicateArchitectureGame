using System.Collections.Generic;
using System.Linq;
using Enemy;
using Hero;
using Infrastructure.AssetManagement;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.ProgressDescription;
using Infrastructure.Services.StaticData;
using Logic;
using StaticData;
using UI.Elements;
using UI.Services.Factory;
using UI.Services.Windows;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using Task = System.Threading.Tasks.Task;

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
        private IAssetProvider _assetProvider;

        public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IProgressDescriptionService progressDescriptionService, IStaticDataService staticData, IWindowService windowsService, IUIFactory uiFactory, IUnearnedLootService unearnedLootService, IAssetProvider assetProvider)
        {
            _staticData = staticData;
            _windowsService = windowsService;
            _uiFactory = uiFactory;
            _unearnedLootService = unearnedLootService;
            _assetProvider = assetProvider;
            _progressDescriptionService = progressDescriptionService;
            _loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string payload)
        {
            _loadingCurtain.Show();
            CleanUpCache();
            WarmUpDependencies();
            
            RegisterServicesAsDataUsers();
            
            _sceneLoader.Load(payload, OnLevelLoaded);
        }

        private void WarmUpDependencies() =>
            _gameFactory.WarmUp();

        private void CleanUpCache()
        {
            _assetProvider.CleanUp();
            _progressDescriptionService.CleanupProgressDataUsersList();
        }

        private async void OnLevelLoaded()
        {
            await InitGameWorld();

            _progressDescriptionService.InformProgressDataReaders();

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

        private async Task InitGameWorld()
        {
            CreateLogger();
            
            InitUIRoot();
            InitUIConsole();

            var levelData = LevelStaticData();
            
            await InitSpawners(levelData);
            await CreateUnearnedLoot();
            
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

        private async Task InitSpawners(LevelStaticData levelStaticData)
        {
            foreach (var spawnerData in levelStaticData.EnemySpawners)
                await _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.ID, spawnerData.MonsterType);
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

        private async Task CreateUnearnedLoot()
        {
            List<LootPieceData> unearnedLootList = _unearnedLootService.GetAll();

            // Copy the list because inside of cycle we add elements inside list which we iterate
            foreach (var lootItem in unearnedLootList.ToList())
            {
                LootPiece loot = await _gameFactory.CreateLoot();
                loot.Initialize(lootItem.Loot, lootItem.Position.AsUnityVector());
            }
            
            unearnedLootList.Clear();
        }

        public void Exit() =>
            _loadingCurtain.Hide();
    }
}