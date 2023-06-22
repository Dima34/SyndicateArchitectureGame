using System;
using UI.Services.Windows;
using UI.Windows;
using UnityEngine;

namespace StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        [SerializeField] private WindowID _windowWindowID;
        [SerializeField] private WindowBase _prefab;

        public WindowID WindowID
        {
            get => _windowWindowID;
            set => _windowWindowID = value;
        }

        public WindowBase Prefab
        {
            get => _prefab;
            set => _prefab = value;
        }
    }
}