using Infrastructure.Data;
using Infrastructure.Services.SaveLoad;
using Infrastructure.States;

namespace Infrastructure.Services.LevelTransferService
{
    public class LevelTransferService : ILevelTransferService
    {
        private IGameStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;
        private string _currentLevel;

        public LevelTransferService(IGameStateMachine stateMachine, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
        }

        public void Transfer(string levelToTransfer)
        {
            _saveLoadService.UpdateAndSaveProgress();
            
            SetAndSaveNewLevelAsCurrent(levelToTransfer);
            
            _stateMachine.Enter<LoadSceneState, string>(levelToTransfer);
        }

        private void SetAndSaveNewLevelAsCurrent(string levelName)
        {
            Progress progress = _saveLoadService.GetProgress();
            progress.WorldData.CurrentLevel = levelName;
            
            _saveLoadService.SaveProgress(progress);
        }
        
    }
}