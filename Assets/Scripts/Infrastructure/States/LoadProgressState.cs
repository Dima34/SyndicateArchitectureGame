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

            playerProgress.WorldData.PositionOnLevel.LevelName = Constants.START_LEVEL_NAME ;

            playerProgress.HeroState.MaxHp = Constants.HERO_MAX_HP;
            playerProgress.HeroState.ResetHP();
            
            return playerProgress;
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