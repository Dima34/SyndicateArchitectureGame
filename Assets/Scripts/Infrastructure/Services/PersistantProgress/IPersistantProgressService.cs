using Infrastructure.Data;
using UnityEditor;
using Progress = Infrastructure.Data.Progress;

namespace Infrastructure.Services.PersistantProgress
{
    public interface IPersistantProgressService : IService
    {
        Progress Progress { get; set; }
    }
}