using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
    class SaveLoadService : ISaveLoadService
    {
        private IGameFactory _gameFactory;
        private IPersistantProgressService _progressService;

        public SaveLoadService(IGameFactory gameFactory, IPersistantProgressService progressService)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (var progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            var jsonedData = _progressService.Progress.ToJson();
            PlayerPrefs.SetString(Constants.PROGRESS_KEY, jsonedData);
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(Constants.PROGRESS_KEY)?
                .ToDeserialized<PlayerProgress>();
        }
    }
}