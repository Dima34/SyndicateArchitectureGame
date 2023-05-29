using System;
using UnityEngine;
using Object = UnityEngine.Object;

// Give an opportunity to start from any scene
class GameRunner : MonoBehaviour
{
    [SerializeField] private GameBootstrapper BootstrapperPrefab;

    private void Awake()
    {
        var gameBootstrapper = FindObjectOfType<GameBootstrapper>();
        if (gameBootstrapper == null)
            Instantiate(BootstrapperPrefab);
    }
}