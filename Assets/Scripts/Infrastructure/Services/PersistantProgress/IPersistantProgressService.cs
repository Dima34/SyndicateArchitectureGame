using Infrastructure.Data;
using UnityEditor;

namespace Infrastructure.Services.PersistantProgress
{
    public interface IPersistantProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}