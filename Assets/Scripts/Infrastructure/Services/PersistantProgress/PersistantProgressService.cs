using Infrastructure.Data;

namespace Infrastructure.Services.PersistantProgress
{
    public class PersistantProgressService : IPersistantProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}