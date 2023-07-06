using UnityEngine;

namespace Infrastructure.States
{
    public class GameProcessService : IGameProcessService
    {
        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
        }
    }
}