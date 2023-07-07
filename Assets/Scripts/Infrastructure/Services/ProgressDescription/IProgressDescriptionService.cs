using Infrastructure.Services.PersistantProgress;

namespace Infrastructure.Services.ProgressDescription
{
    public interface IProgressDescriptionService : IService
    {
        void CleanupProgressDataUsersList();
        void InformProgressDataReaders();
        void RegisterDataReader(ISavedProgressReader progressReader);
        void RegisterDataWriter(ISavedProgressWriter progressWriter);
        void UpdateProgress();
    }
}