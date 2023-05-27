using Services.Inputs;
using UnityEngine;

namespace Infrastructure
{
    class BootstrapState : IState
    {
        private const string INITIAL_SCENE = "Initial";
        private const string MAIN_SCENE_NAME = "Main";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(INITIAL_SCENE, EnterLoadLevel);
        }

        private void RegisterServices()
        {
            RegisterInputService();
        }

        private void RegisterInputService()
        {
            Game.InputService = GetInputService();
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(MAIN_SCENE_NAME);
        }

        public void Exit()
        {
        }

        private static IInputService GetInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputServise();
        }
    }
}