using System.Linq;
using Enemy;
using Hero;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistantProgress;
using Infrastructure.Services.ProgressDescription;
using Logic;
using UI;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private IProgressDescriptionService _progressDescriptionService;
        private IPersistantProgressService _progressService;

        public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IProgressDescriptionService progressDescriptionService,
            IPersistantProgressService progressService)
        {
            _progressService = progressService;
            _progressDescriptionService = progressDescriptionService;
            _loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string payload)
        {
            _loadingCurtain.Show();
            _progressDescriptionService.CleanupProgressMembersList();
            sceneLoader.Load(payload, OnLevelLoaded);
        }

        private void OnLevelLoaded()
        {
            InitGameWorld();

            _progressDescriptionService.InformProgressDataReaders();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            InitSpawners();
            CreateUnearnedLoot();
            GameObject hero = CreatePlayer();
            CreateHUD(hero);
        }

        private void InitSpawners()
        {
            foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(Constants.ENEMY_SPAWNER_TAG))
                _gameFactory.RegisterDataUsers(spawnerObject);
        }

        private GameObject CreatePlayer()
        {
            var initialPoint = GameObject.FindGameObjectWithTag(Constants.INITIAL_POINT_TAG);
            var player = _gameFactory.CreateHero(initialPoint.transform.position);

            CameraFollow(player);

            return player;
        }

        private static void CameraFollow(GameObject player) =>
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(player);

        private void CreateHUD(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHUD();

            HeroHealth heroHealth = hero.GetComponent<HeroHealth>();
            hud.GetComponentInChildren<ActorUI>().Construct(heroHealth);
        }

        private void CreateUnearnedLoot()
        {
            WorldData worldData = _progressService.Progress.WorldData;
            var unearnedLootDict =
                worldData.UnEarnedLootpieces.LootPieces;

            foreach (var lootItem in unearnedLootDict)
            {
                LootPiece loot = _gameFactory.CreateLoot(); 
                loot.Initialize(lootItem.Loot);

                loot.transform.position = lootItem.Position.AsUnityVector();
            }
        }

        public void Exit() =>
            _loadingCurtain.Hide();
    }
}