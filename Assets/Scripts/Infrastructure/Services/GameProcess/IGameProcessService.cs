using Infrastructure.Services;

namespace Infrastructure.States
{
    public interface IGameProcessService : IService
    {
        void PauseGame();
        void ResumeGame();
    }
}