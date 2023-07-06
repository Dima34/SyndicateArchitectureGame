using Infrastructure.Data;

namespace Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        Progress LoadProgress();
        void SaveProgress();
    }
    
}