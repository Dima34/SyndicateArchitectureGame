using Infrastructure;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services.Inputs;
using UnityEngine;

public class Game
{
    public readonly GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
    {
        var services = AllServices.Container;
        StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, services);
    }
}