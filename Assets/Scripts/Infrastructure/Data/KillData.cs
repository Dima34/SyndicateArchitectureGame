using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class KillData
    {
        [SerializeField] private List<string> _clearedSpawners = new List<string>();

        public List<string> ClearedSpawners
        {
            get => _clearedSpawners;
            set => _clearedSpawners = value;
        }
    }
}