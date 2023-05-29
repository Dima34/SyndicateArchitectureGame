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

        private PlayerProgress NewProgress() => 
            new PlayerProgress(Constants.MAIN_LEVEL_NAME);

        private void LoadCurrentLevel(string currentLevel)
        {
            _stateMachine.Enter<LoadLevelState, string>(currentLevel);
        }

        public void Exit()
        {
        }
    }
}