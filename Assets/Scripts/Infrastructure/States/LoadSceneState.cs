using Infrastructure.Factory;
using Infrastructure.Services.PersistantProgress;
using Logic;
using Player;
using UI;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private const string INITIAL_POINT_TAG = "InitialPoint";
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
            InitGameWorld();
            InformProgressDataReaders();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            GameObject hero = CreatePlayer();
            CreateHUD(hero);
        }

        private GameObject CreatePlayer()
        {
            var initialPoint = GameObject.FindGameObjectWithTag(INITIAL_POINT_TAG);
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