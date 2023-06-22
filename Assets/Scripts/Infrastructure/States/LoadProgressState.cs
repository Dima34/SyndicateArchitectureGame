using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.PersistantProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
using StaticData;

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
            SetDefaultHeroState(playerProgress);
            
            return playerProgress;
        }

        private void SetDefaultHeroState(PlayerProgress playerProgress)
        {
            HeroStaticData staticData = _staticDataService.Hero();
            
            playerProgress.HeroStats.Damage = staticData.Damage;
            playerProgress.HeroStats.HitRadius = staticData.HitRadius;
            playerProgress.HeroStats.HitForwardOffset = staticData.HitForwardOffset;
            playerProgress.HeroStats.HitObjectPerHit = staticData.HitObjectPerHit;

            playerProgress.HeroStats.MaxHp = staticData.MaxHp;
            playerProgress.HeroStats.CurrentHp = staticData.MaxHp;
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