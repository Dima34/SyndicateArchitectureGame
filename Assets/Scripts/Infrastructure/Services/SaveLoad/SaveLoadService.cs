using Infrastructure.Data;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
    class SaveLoadService : ISaveLoadService
    {
        public PlayerProgress LoadProgress()
        {
            return new PlayerProgress(Constants.MAIN_LEVEL_NAME);
        }

        public void SaveProgress()
        {
            PlayerPrefs.GetString(Constants.PROGRESS_KEY)?.ToDeserialized<PlayerProgress>(); 
        }
    }
}