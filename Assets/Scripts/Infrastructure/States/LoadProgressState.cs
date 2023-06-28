using System.Collections.Generic;
using Infrastructure.Data;
using Infrastructure.Services.PersistantProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
using StaticData;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private IPersistantProgressService _progressService;
        private GameStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;
        private IStaticDataService _staticDataService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistantProgressService progressService,
            ISaveLoadService saveLoadService, IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            var currentLevel = _progressService.Progress.WorldData.CurrentLevel;

            LoadCurrentLevel(currentLevel);
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private Progress NewProgress()
        {
            var playerProgress = new Progress();
            
            FillLevelData(playerProgress);
            InitUnearnedPices(playerProgress);
            SetDefaultHeroState(playerProgress);
            
            return playerProgress;
        }

        private void FillLevelData(Progress progress)
        {
            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCountInBuildSettings; sceneIndex++)
            {
                var scene = SceneManager.GetSceneByBuildIndex(sceneIndex);
                SceneSearch
                progress.WorldData.LevelsData.Add(new LevelData(scene.name));
            }

            progress.WorldData.CurrentLevel = Constants.START_LEVEL_NAME;
        }

        private static void InitUnearnedPices(Progress playerProgress) =>
            playerProgress.WorldData.GetCurrentLevelData().UnEarnedLootPieces = new List<LootPieceData>();

        private void SetDefaultHeroState(Progress progress)
        {
            HeroStaticData staticData = _staticDataService.Hero();
            
            progress.HeroStats.Damage = staticData.Damage;
            progress.HeroStats.HitRadius = staticData.HitRadius;
            progress.HeroStats.HitForwardOffset = staticData.HitForwardOffset;
            progress.HeroStats.HitObjectPerHit = staticData.HitObjectPerHit;

            progress.HeroStats.MaxHp = staticData.MaxHp;
            progress.HeroStats.CurrentHp = staticData.MaxHp;
        }

        private void LoadCurrentLevel(string currentLevel) =>
            _stateMachine.Enter<LoadSceneState, string>(currentLevel);

        public void Exit()
        {
        }
    }
}