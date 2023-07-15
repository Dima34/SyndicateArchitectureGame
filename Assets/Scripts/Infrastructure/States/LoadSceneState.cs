using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enemy;
using Hero;
using Infrastructure.AssetManagement;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistantProgress;
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
        private IPersistantProgressService _progresService;

        public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IProgressDescriptionService progressDescriptionService, IStaticDataService staticData, IWindowService windowsService, IUIFactory uiFactory, IUnearnedLootService unearnedLootService, IAssetProvider assetProvider, IPersistantProgressService progresService)
        {
            _staticData = staticData;
            _windowsService = windowsService;
            _uiFactory = uiFactory;
            _unearnedLootService = unearnedLootService;
            _assetProvider = assetProvider;
            _progresService = progresService;
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
            
            InformDataReaders();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformDataReaders() =>
            _progressDescriptionService.InformProgressDataReaders();

        private void RegisterServicesAsDataUsers()
        {
            RegisterUnearnedLootService();
        }
        
        private void RegisterUnearnedLootService()
        {
            _progressDescriptionService.RegisterDataWriter(_unearnedLootService);
        }

        private async Task InitGameWorld()
        {
            CreateLogger();
            
            await InitUIRoot();
            InitUIConsole();

            var levelData = LevelStaticData();
            
            GameObject cameraGO = await CreateCamera();
            GameObject hero = await CreatePlayer();
            CameraFollow(hero, cameraGO);
            
            await InitSpawners(levelData);
            await CreateUnearnedLoot();

            await CreateHUD(hero);
        }

        private async Task<GameObject> CreateCamera()
        {
            GameObject cameraGameObject = await _assetProvider.Load<GameObject>(AssetPath.CAMERA);
            return Object.Instantiate(cameraGameObject);
        }

        private static void CameraFollow(GameObject hero, GameObject cameraGO)
        {
            CameraFollow cameraFollow = cameraGO.GetComponent<CameraFollow>();
            cameraFollow.Follow(hero);
        }

        private void CreateLogger() =>
            _gameFactory.CreateLogger();

        private async Task InitUIRoot() =>
            await _uiFactory.CreatUIRoot();

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

        private async Task<GameObject> CreatePlayer() =>
            await _gameFactory.CreateHero();
        

        private async Task CreateHUD(GameObject hero)
        {
            GameObject hud = await _gameFactory.CreateHUD();
            
            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(_windowsService);

            HeroHealth heroHealth = hero.GetComponent<HeroHealth>();
            hud.GetComponentInChildren<ActorUI>().Construct(heroHealth);
        }

        private async Task CreateUnearnedLoot()
        {
            List<LootPieceData> unearnedLootList =
                _progresService.Progress.WorldData.GetCurrentLevelData().UnEarnedLootPieces;

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