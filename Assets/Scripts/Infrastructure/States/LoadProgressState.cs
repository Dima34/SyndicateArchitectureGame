using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.PersistantProgress;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private IPersistantProgressService _progressService;
        private GameStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistantProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            var currentLevel = _progressService.Progress.WorldData.PositionOnLevel.LevelName;

            LoadCurrentLevel(currentLevel);
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            var playerProgress = new PlayerProgress();

            SetDefaultLevel(playerProgress);
            SetDefaultHeroStats(playerProgress);
            SetDefaultHeroState(playerProgress);
            
            return playerProgress;
        }

        private void SetDefaultHeroState(PlayerProgress playerProgress)
        {
            playerProgress.HeroState.MaxHp = Constants.HERO_MAX_HP;
            playerProgress.HeroState.ResetHP();
        }

        private void SetDefaultHeroStats(PlayerProgress playerProgress)
        {
            playerProgress.HeroStats.Damage = 2f;
            playerProgress.HeroStats.HitRadius = 2f;
            playerProgress.HeroStats.HitForwardOffset = 3f;
        }

        private void SetDefaultLevel(PlayerProgress playerProgress)
        {
            playerProgress.WorldData.PositionOnLevel.LevelName = Constants.START_LEVEL_NAME;
        }

        private void LoadCurrentLevel(string currentLevel)
        {
            _stateMachine.Enter<LoadSceneState, string>(currentLevel);
        }

        public void Exit()
        {
        }
    }
}