using Infrastructure.Data;

namespace Infrastructure.Services.PersistantProgress
{
    public interface ISavedProgressWriter
    {
        void UpdateProgress(PlayerProgress progress);
    }
}