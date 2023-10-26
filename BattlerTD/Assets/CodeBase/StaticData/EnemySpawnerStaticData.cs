using System;
using UnityEngine;

namespace CodeBase.StaticData
{
  [Serializable]
  public class EnemySpawnerStaticData
  {
    public string Id;
    public MonsterTypeId MonsterTypeId;
    public Vector3 Position;
    public Quaternion Rotation;

    public EnemySpawnerStaticData(string id, MonsterTypeId monsterTypeId, Vector3 position, Quaternion rotation)
    {
      Id = id;
      MonsterTypeId = monsterTypeId;
      Position = position;
      Rotation = rotation;
    }
  }
}