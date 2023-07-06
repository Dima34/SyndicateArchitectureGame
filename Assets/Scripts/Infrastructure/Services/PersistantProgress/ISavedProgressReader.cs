using Infrastructure.Data;

namespace Infrastructure.Services.PersistantProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(Progress progress);
    }
}