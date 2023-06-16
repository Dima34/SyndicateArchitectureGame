using Enemy;
using Hero;
using Infrastructure.Factory;
using Infrastructure.Services.PersistantProgress;
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
        private IPersistantProgressService _persistantProgressService;

        public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistantProgressService persistantProgressService)
        {
            _persistantProgressService = persistantProgressService;
            _loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string payload)
        {
            _loadingCurtain.Show();
            _gameFactory.CleanupProgressMembersList();
            sceneLoader.Load(payload, OnLevelLoaded);
        }

        private void OnLevelLoaded()
        {
            InitSpawners();
            InitGameWorld();
            InformProgressDataReaders();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitSpawners()
        {
            foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(Constants.ENEMY_SPAWNER_TAG))
                _gameFactory.RegisterDataUsers(spawnerObject);
        }

        private void InitGameWorld()
        {
            GameObject hero = CreatePlayer();
            CreateHUD(hero);
        }

        private GameObject CreatePlayer()
        {
            var initialPoint = GameObject.FindGameObjectWithTag(Constants.INITIAL_POINT_TAG);
            var player = _gameFactory.CreateHero(initialPoint.transform.position);

            CameraFollow(player);

            return player;
        }

        private void CreateHUD(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHUD();

            HeroHealth heroHealth = hero.GetComponent<HeroHealth>();
            hud.GetComponentInChildren<ActorUI>().Construct(heroHealth);
        }

        private static void CameraFollow(GameObject player) =>
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(player);

        private void InformProgressDataReaders()
        {
            foreach (var savedProgressReader in _gameFactory.ProgressReaders)
                savedProgressReader.LoadProgress(_persistantProgressService.Progress);
        }

        public void Exit() =>
            _loadingCurtain.Hide();
    }
}