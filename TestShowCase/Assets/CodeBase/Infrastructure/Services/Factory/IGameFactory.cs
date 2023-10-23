using System;
using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
  public interface IGameFactory : IService, IDisposable
  {
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    GameObject CreateHero(GameObject at);
    GameObject CreateHud();
    GameObject CreateMonster(MonsterTypeId typeId, Transform parent, float progressMultiplier);
    GameObject CreateBoss(MonsterTypeId typeId, Transform parent, float progressMultiplier);
    LootPiece CreateLoot();
    void CreateSpawner(string spawnerId, Vector3 at, MonsterTypeId monsterTypeId, Transform parent);
    void Cleanup();

    GameObject HeroGameObject { get; }
    BossSpawnPoint BossSpawner { get; }
    List<SpawnPoint> Spawners { get; }
    void CreateBossSpawner(Vector3 at, MonsterTypeId monsterTypeId, Transform parent);
  }
}