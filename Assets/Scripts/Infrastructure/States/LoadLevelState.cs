using Infrastructure.Factory;
using Infrastructure.Services.PersistantProgress;
using Logic;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string INITIAL_POINT_TAG = "InitialPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private IPersistantProgressService _persistantProgressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
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
            _gameFactory.CreateHUD();
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            var initialPoint = GameObject.FindGameObjectWithTag(INITIAL_POINT_TAG);
            var player = _gameFactory.CreateHero(initialPoint.transform.position);

            CameraFollow(player);
        }

        private static void CameraFollow(GameObject player)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(player);
        }

        private void InformProgressDataReaders()
        {
            foreach (var savedProgressReader in _gameFactory.ProgressReaders)
            {
                savedProgressReader.LoadProgress(_persistantProgressService.Progress);
            }
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }
    }
}