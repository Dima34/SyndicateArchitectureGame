using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class PositionOnLevel
    {
        [SerializeField]
        private string _levelName;
        [SerializeField]
        private Vector3Data _position;

        public string LevelName
        {
            get => _levelName;
            set => _levelName = value;
        }
        
        public Vector3Data Position
        {
            get => _position;
            set => _position = value;
        }

        public PositionOnLevel(string levelName, Vector3Data position)
        {
            _position = position;
            _levelName = levelName;
        }
        
        public PositionOnLevel()
        { }
    }
}