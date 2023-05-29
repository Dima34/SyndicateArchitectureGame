using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class Vector3Data
    {
        [SerializeField]
        private float _x;
        [SerializeField]
        private float _y;
        [SerializeField]
        private float _z;

        public float X
        {
            get => _x;
            set => _x = value;
        }

        public float Y
        {
            get => _y;
            set => _y = value;
        }

        public float Z
        {
            get => _z;
            set => _z = value;
        }
        
        public Vector3Data(float x, float y, float z)
        {
            _z = z;
            _y = y;
            _x = x;
        }
    }
}