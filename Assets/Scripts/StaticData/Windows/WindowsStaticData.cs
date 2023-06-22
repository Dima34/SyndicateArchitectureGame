using System.Collections.Generic;
using UnityEngine;

namespace StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowsData", menuName = "StaticData/Windows ")]
    public class WindowsStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}