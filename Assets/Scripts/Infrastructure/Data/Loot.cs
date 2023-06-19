using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class Loot
    {
        [SerializeField] private int _value;

        public int Value
        {
            get => _value;
            set => _value = value;
        }
    }
}