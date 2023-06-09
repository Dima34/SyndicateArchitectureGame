using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistantProgress;
using Infrastructure.Services.ProgressDescription;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
using Infrastructure.States;
using Logic;
using StaticData;
using UI.Services.Factory;
using UI.Services.Windows;

namespace Infrastructure
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoder, LoadingCurtain loadingCurtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoder, services),
                [typeof(LoadSceneState)] = new LoadSceneState(this, 
                    sceneLoder, 
                    loadingCurtain, 
                    services.GetSingle<IGameFactory>(),
                    services.GetSingle<IProgressDescriptionService>(),
                    services.GetSingle<IUnearnedLootService>(),
                    services.GetSingle<IStaticDataService>(),
                    services.GetSingle<IWindowService>(),
                    services.GetSingle<IUIFactory>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, 
                    services.GetSingle<IPersistantProgressService>(),
                    services.GetSingle<ISaveLoadService>(),
                    services.GetSingle<IStaticDataService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeActiveAndGetNewState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeActiveAndGetNewState<TState>();
            state.Enter(payload);
        }

        private TState ChangeActiveAndGetNewState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class => 
            _states[typeof(TState)] as TState;
    }
}