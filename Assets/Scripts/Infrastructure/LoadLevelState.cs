using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string INITIAL_POINT_TAG = "InitialPoint";
        private const string HERO_PATH = "Hero/Player";
        private const string HUD_PATH = "UI/HUD";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoder;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoder)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoder = sceneLoder;
        }

        public void Enter(string payload)
        {
            _sceneLoder.Load(payload, OnLevelLoaded);
            
        }

        private void OnLevelLoaded()
        {
            CreateHUD();
            CreatePlayer();
        }

        private void CreateHUD()
        {
            var HUD = InstantiateResourse(HUD_PATH);
        }

        private void CreatePlayer()
        {
            var initialPoint = GameObject.FindGameObjectWithTag(INITIAL_POINT_TAG);
            var player = InstantiateResourse(HERO_PATH, initialPoint.transform.position);
            
            CameraFollow(player);
        }

        private static GameObject InstantiateResourse(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private static GameObject InstantiateResourse(string path, Vector3 at)
        {
            var instantiatedPrefab = InstantiateResourse(path);

            instantiatedPrefab.transform.position = at;
            
            return instantiatedPrefab;
        }
        
        

        private static void CameraFollow(GameObject player)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(player);
        }

        public void Exit()
        {
        }
    }
}