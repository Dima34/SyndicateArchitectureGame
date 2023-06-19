using System.Collections.Generic;
using Infrastructure.Services.PersistantProgress;

namespace Infrastructure.Services.ProgressDescription
{
    public class ProgressDescriptionService : IProgressDescriptionService
    {
        private List<ISavedProgressReader> _progressReaders = new List<ISavedProgressReader>();
        private List<ISavedProgressWriter> _progressWriters = new List<ISavedProgressWriter>();

        private IPersistantProgressService _persistantProgressService;

        public ProgressDescriptionService(IPersistantProgressService persistantProgressService) =>
            _persistantProgressService = persistantProgressService;

        public void CleanupProgressMembersList()
        {
            _progressReaders.Clear();
            _progressWriters.Clear();
        }

        public void InformProgressDataReaders()
        {
            foreach (var savedProgressReader in _progressReaders)
                savedProgressReader.LoadProgress(_persistantProgressService.Progress);
        }

        public void RegisterDataReader(ISavedProgressReader progressReader) =>
            _progressReaders.Add(progressReader);
        
        public void RegisterDataWriter(ISavedProgressWriter progressWriter) =>
            _progressWriters.Add(progressWriter);
        
        public void UnRegisterDataReader(ISavedProgressReader progressReader) =>
            _progressReaders.Remove(progressReader);
        
        public void UnRegisterDataWriter(ISavedProgressWriter progressWriter) =>
            _progressWriters.Remove(progressWriter);

        public void UpdateProgress()
        {
            foreach (var progressWriter in _progressWriters)
                progressWriter.UpdateProgress(_persistantProgressService.Progress);
        }
    }
}