using System;
using Infrastructure.Data;

namespace Infrastructure.Services.PersistantProgress
{
    [Serializable]
    public class PersistantProgressService : IPersistantProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}