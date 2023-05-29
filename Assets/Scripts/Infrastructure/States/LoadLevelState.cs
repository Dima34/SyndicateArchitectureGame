using Infrastructure.Factory;
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

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory)
        {
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
            _gameFactory.CreateHUD();
            CreatePlayer();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CreatePlayer()
        {
            var initialPoint = GameObject.FindGameObjectWithTag(INITIAL_POINT_TAG);
            var player = _gameFactory.CreateHero(initialPoint);

            CameraFollow(player);
        }

        private static void CameraFollow(GameObject player)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(player);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }
    }
}