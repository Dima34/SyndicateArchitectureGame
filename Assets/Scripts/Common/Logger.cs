using System;
using UnityEngine;

namespace Common
{
    public class Logger : MonoBehaviour
    {
        public static Logger Instance { get; set; }
        
        private void Awake()
        {
            Instance = this;
        }

        public static void Log(string message) =>
            Debug.Log(message);

        public static void LogWarn(string message) =>
            Debug.LogWarning(message);
        
        public static void LogErr(string message) =>
            Debug.LogError(message);
    }
}