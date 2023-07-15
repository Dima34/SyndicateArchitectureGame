using System.Linq;
using Infrastructure;
using Infrastructure.Logic;
using Logic.EnemySpawners;
using StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniqueId = Logic.UniqueId;

[CustomEditor(typeof(LevelStaticData))]
public class LevelStaticDataEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelStaticData levelData = (LevelStaticData)target;

        if (GUILayout.Button("Collect"))
        {
            levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
                .Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().Id, x.MonsterTypeId, x.transform.position))
                .ToList();

            levelData.LevelKey = SceneManager.GetActiveScene().name;
            levelData.InitialHeroPosition = GameObject.FindGameObjectWithTag(Constants.INITIAL_POINT_TAG).transform.position;
            
            EditorUtility.SetDirty(levelData);
        }
    }
}