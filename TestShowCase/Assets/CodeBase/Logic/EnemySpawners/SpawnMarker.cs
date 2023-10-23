using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
  public class SpawnMarker : MonoBehaviour
  {
    public MonsterTypeId MonsterTypeId;

    public Vector3 modelOffset;
    public GameObject mobModelPrefab;
  }
}