using System;
using System.Linq;
using Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (UniqueId)target;

            if (string.IsNullOrEmpty(uniqueId.Id))
                GenerateId(uniqueId);
            else
            {
                UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();
                
                if(uniqueIds.Any(other => other.Id == uniqueId.Id &&other != uniqueId))
                    GenerateId(uniqueId);
            }
        }

        private void GenerateId(UniqueId uniqueId)
        {
            if (!Application.isPlaying)
            {
                uniqueId.Id = uniqueId.gameObject.scene.name + "_" + Guid.NewGuid();
                
                // Because we changed unique id above we need to save changes
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }
    }
}