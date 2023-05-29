using Infrastructure;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.States;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    private Game _game;
    [SerializeField]
    private LoadingCurtain _loadingCurtain;

    private void Awake()
    {
        _game = new Game(this, _loadingCurtain);
        _game.StateMachine.Enter<BootstrapState>();
        
        DontDestroyOnLoad(this);
    }
}
