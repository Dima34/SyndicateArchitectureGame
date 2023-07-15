using Infrastructure.Services;

namespace Infrastructure
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        TState ChangeActiveAndGetNewState<TState>() where TState : class, IExitableState;
        TState GetState<TState>() where TState : class;
    }
}