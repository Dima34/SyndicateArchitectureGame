using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.LevelTransferService;
using Infrastructure.Services.PersistantProgress;
using Infrastructure.Services.ProgressDescription;
using Infrastructure.Services.Random;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
using Services.Inputs;
using UI.Services.Factory;
using UI.Services.Windows;
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

            RegisterSingle<IGameStateMachine>(_stateMachine);
            RegisterSingle<IGameProcessService>(new GameProcessService());
            RegisterSingle<IPersistantProgressService>(new PersistantProgressService());
            RegisterSingle<IUnearnedLootService>(new UnearnedLootService());
            RegisterSingle<IProgressDescriptionService>(new ProgressDescriptionService(GetSingle<IPersistantProgressService>()));
            RegisterSingle<ISaveLoadService>(new SaveLoadService(
                GetSingle<IPersistantProgressService>(),
                GetSingle<IProgressDescriptionService>()));
            RegisterSingle<ILevelTransferService>(new LevelTransferService(
                GetSingle<IGameStateMachine>(),
                GetSingle<ISaveLoadService>()));
            RegisterAssetProvider();
            RegisterSingle<IInputService>(GetInputService());
            RegisterSingle<IRandomService>(new RandomService());
            RegisterSingle<IAdsService>(new AdsService());
            
            RegisterSingle<IUIFactory>(new UIFactory(
                GetSingle<IAssetProvider>(),
                GetSingle<IStaticDataService>(),
                GetSingle<IPersistantProgressService>(),
                GetSingle<IAdsService>(), GetSingle<IGameProcessService>()));
            RegisterSingle<IWindowService>(new WindowService(GetSingle<IUIFactory>()));

            RegisterGameFactory();
        }

        private void RegisterAssetProvider()
        {
            var newAssetProvider = new AssetProvider();
            newAssetProvider.Initialize();
            
            RegisterSingle<IAssetProvider>(newAssetProvider);
        }

        private void RegisterSingle<T>(T objectToRegister) where T : IService =>
            _services.RegisterSingle<T>(objectToRegister);

        private T GetSingle<T>() where T : IService =>
            _services.GetSingle<T>();

        private void RegisterGameFactory()
        {
            RegisterSingle<IGameFactory>(new GameFactory(
                GetSingle<IAssetProvider>(),
                GetSingle<IStaticDataService>(),
                GetSingle<IInputService>(),
                GetSingle<IRandomService>(),
                GetSingle<IPersistantProgressService>(),
                GetSingle<IProgressDescriptionService>(),
                GetSingle<IUnearnedLootService>()
            ));
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticData = new StaticDataService();

            staticData.LoadStaticData();

            RegisterSingle<IStaticDataService>(staticData);
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