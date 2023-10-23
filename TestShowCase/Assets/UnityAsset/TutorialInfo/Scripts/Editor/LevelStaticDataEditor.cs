using System.Linq;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    private const string PlayerInitialPoint = "InitialPoint";
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LevelStaticData levelData = (LevelStaticData) target;

      if (GUILayout.Button("Collect"))
      {
        levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
          .Select(x => new EnemySpawnerStaticData(x.GetComponent<UniqueId>().Id, x.MonsterTypeId, x.transform.position))
          .ToList();

        levelData.LevelKey = SceneManager.GetActiveScene().name;

        levelData.BossSpawnerPosition = FindObjectOfType<BossSpawnMarker>().transform.position;
        
        levelData.InitialHeroPosition = GameObject.FindWithTag(PlayerInitialPoint).transform.position;
      }
      
      EditorUtility.SetDirty(target);
    }
  }
}