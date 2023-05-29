using Infrastructure.States;
using Logic;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    private Game _game;
    [SerializeField]
    private LoadingCurtain _loadingCurtainPrefab;

    private void Awake()
    {
        var curtain = Instantiate(_loadingCurtainPrefab);
        _game = new Game(this, curtain);
        _game.StateMachine.Enter<BootstrapState>();
        
        DontDestroyOnLoad(this);
    }
}