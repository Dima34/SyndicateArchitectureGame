using Infrastructure.Services.PersistantProgress;

namespace Infrastructure.Services.ProgressDescription
{
    public interface IProgressDescriptionService : IService
    {
        void CleanupProgressMembersList();
        void InformProgressDataReaders();
        void RegisterDataReader(ISavedProgressReader progressReader);
        void RegisterDataWriter(ISavedProgressWriter progressWriter);
        void UpdateProgress();
        void UnRegisterDataWriter(ISavedProgressWriter progressWriter);
        void UnRegisterDataReader(ISavedProgressReader progressReader);
    }
}