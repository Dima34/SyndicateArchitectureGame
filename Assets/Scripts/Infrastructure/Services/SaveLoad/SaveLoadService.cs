using Infrastructure.Data;
using Infrastructure.Services.PersistantProgress;
using Infrastructure.Services.ProgressDescription;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
    class SaveLoadService : ISaveLoadService
    {
        private IPersistantProgressService _progressService;
        private IProgressDescriptionService _progressDescriptionService;

        public SaveLoadService(IPersistantProgressService progressService, IProgressDescriptionService progressDescriptionService)
        {
            _progressService = progressService;
            _progressDescriptionService = progressDescriptionService;
        }

        public void SaveProgress()
        {
            _progressDescriptionService.UpdateProgress();
            
            var jsonedData = GetProgress().ToJson();
            PlayerPrefs.SetString(Constants.PROGRESS_KEY, jsonedData);
        }

        private Progress GetProgress() =>
            _progressService.Progress;

        public Progress LoadProgress()
        {
            return PlayerPrefs.GetString(Constants.PROGRESS_KEY)?
                .ToDeserialized<Progress>();
        }
    }
}