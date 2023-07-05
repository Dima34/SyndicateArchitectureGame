namespace Infrastructure.States
{
    public interface IGameProcessService
    {
        void PauseGame();
        void ResumeGame();
    }
}