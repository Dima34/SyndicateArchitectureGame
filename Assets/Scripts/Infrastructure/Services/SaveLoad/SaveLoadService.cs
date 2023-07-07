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

        public void UpdateAndSaveProgress()
        {
            _progressDescriptionService.UpdateProgress();

            Progress progress = GetProgress();
            SaveProgress(progress);
        }

        public void SaveProgress(Progress progress)
        {
            var jsonedData = progress.ToJson();
            PlayerPrefs.SetString(Constants.PROGRESS_KEY, jsonedData);
        }

        public Progress GetProgress() =>
            _progressService.Progress;

        public Progress LoadProgress()
        {
            return PlayerPrefs.GetString(Constants.PROGRESS_KEY)?
                .ToDeserialized<Progress>();
        }
    }
}