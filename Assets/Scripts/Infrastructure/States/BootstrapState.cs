using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistantProgress;
using Infrastructure.Services.ProgressDescription;
using Infrastructure.Services.Random;
using Infrastructure.Services.SaveLoad;
using Services.Inputs;
using StaticData;
using UnityEngine;

namespace Infrastructure.States
{
    class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        private void RegisterServices()
        {
            RegisterStaticDataService();
            
            _services.RegisterSingle<IPersistantProgressService>(new PersistantProgressService());
            _services.RegisterSingle<IProgressDescriptionService>(new ProgressDescriptionService(_services.GetSingle<IPersistantProgressService>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.GetSingle<IPersistantProgressService>(), _services.GetSingle<IProgressDescriptionService>()));
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IInputService>(GetInputService());
            _services.RegisterSingle<IRandomService>(new RandomService());
            _services.RegisterSingle<IUnearnedLootService>(new UnearnedLootService());
            
            RegisterGameFactory();
        }

        private void RegisterGameFactory()
        {
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.GetSingle<IAssetProvider>(), 
                _services.GetSingle<IStaticDataService>(),
                _services.GetSingle<IInputService>(),
                _services.GetSingle<IRandomService>(),
                _services.GetSingle<IPersistantProgressService>(),
                _services.GetSingle<IProgressDescriptionService>(),
                _services.GetSingle<IUnearnedLootService>()
                ));
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadMonsters();
            _services.RegisterSingle<IStaticDataService>(staticData);
        }

        public void Enter()
        {
            _sceneLoader.Load(Constants.INITIAL_SCENE, EnterLoadProgressState);
        }

        private void EnterLoadProgressState()
        {
            _stateMachine.Enter<LoadProgressState>();
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