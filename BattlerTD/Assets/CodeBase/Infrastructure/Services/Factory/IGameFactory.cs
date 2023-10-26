using System;
using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.Tower;
using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
	public interface IGameFactory : IService, IDisposable
	{
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		GameObject CreateHero(Vector3 at);
		Hud CreateHud();
		GameObject CreateMonster(MonsterTypeId typeId, Transform parent, float progressMultiplier = 1);
		GameObject CreateBoss(MonsterTypeId typeId, Transform parent, float progressMultiplier = 1);
		LootPiece CreateLoot();
		void CreateSpawner(string spawnerId, Vector3 at, Quaternion rotation, MonsterTypeId monsterTypeId);
		void Cleanup();

		GameObject MainPumpkinGameObject { get; }
		GameObject HeroGameObject { get; }
		BossSpawnPoint BossSpawner { get; }
		List<SpawnPoint> Spawners { get; }
		List<GameObject> Monsters { get; }
		Action<GameObject> MonsterCreated { get; set; }
		Hud HUD { get; }
		void CreateBossSpawner(Vector3 at, MonsterTypeId monsterTypeId, Transform parent);
		GameObject CreateKing(Vector3 at);
		Grid CreateGrid();
		GameObject CreateTower(TowerType towerType, Vector3 position, Quaternion rotation);
	}

}