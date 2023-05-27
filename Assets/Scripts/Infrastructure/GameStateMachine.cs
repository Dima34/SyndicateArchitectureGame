using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoder)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoder),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoder)
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