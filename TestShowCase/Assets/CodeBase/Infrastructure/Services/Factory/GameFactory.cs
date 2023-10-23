using System;
using System.Collections.Generic;
using CodeBase.AssetManagement;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Services.Factory
{
	public class GameFactory : IGameFactory
	{
		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

		private readonly IAssetProvider _assets;
		private readonly IStaticDataService _staticData;
		private readonly IRandomService _randomService;
		private readonly IPersistentProgressService _persistentProgressService;
		private readonly IWindowService _windowService;
		private readonly IInputService _inputService;
		private readonly List<SpawnPoint> _spawners = new List<SpawnPoint>();
		private GameObject _heroGameObject;

		private List<GameObject> Monsters = new List<GameObject>();
		private BossSpawnPoint _bossSpawner;
		private ITimerService _timerService;

		public GameObject HeroGameObject => _heroGameObject;
		public BossSpawnPoint BossSpawner => _bossSpawner;
		public List<SpawnPoint> Spawners => _spawners;

		public GameFactory(IAssetProvider assets, IStaticDataService staticData, IRandomService randomService, IPersistentProgressService persistentProgressService,
			IWindowService windowService, ITimerService timerService, IInputService inputService)
		{
			_assets = assets;
			_staticData = staticData;
			_randomService = randomService;
			_persistentProgressService = persistentProgressService;
			_windowService = windowService;
			_timerService = timerService;
			_inputService = inputService;
		}

		public GameObject CreateHero(GameObject at)
		{
			_heroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
			_heroGameObject.GetComponent<HeroMove>().Construct(_timerService, _inputService);
			return _heroGameObject;
		}

		public GameObject CreateHud()
		{
			GameObject hud = InstantiateRegistered(AssetPath.HudPath);

			hud.GetComponentInChildren<LootCounter>()
				.Construct(_persistentProgressService.Progress.WorldData);

			foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
				openWindowButton.Init(_windowService);

			return hud;
		}

		public LootPiece CreateLoot()
		{
			LootPiece lootPiece = InstantiateRegistered(AssetPath.Loot)
				.GetComponent<LootPiece>();

			Debug.Log(lootPiece);

			lootPiece.Construct(_persistentProgressService.Progress.WorldData);

			return lootPiece;
		}

		public GameObject CreateBoss(MonsterTypeId typeId, Transform parent, float progressMultiplier)
		{
			MonsterStaticData monsterData = _staticData.ForMonster(typeId);
			Vector3 spawnPosition = new Vector3(parent.position.x + 17, parent.position.y);
			GameObject monster = Object.Instantiate(monsterData.Prefab, spawnPosition, Quaternion.identity);

			IHealth health = monster.GetComponent<IHealth>();
			health.Current = monsterData.Hp;
			health.Max = monsterData.Hp;

			monster.GetComponent<ActorUI>()?.Construct(health, _persistentProgressService);
			monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

			Attack attack = monster.GetComponentInChildren<Attack>();
			attack.Construct(_heroGameObject.transform);
			attack.Damage = monsterData.Damage * progressMultiplier;
			attack.Cleavage = monsterData.Cleavage;
			attack.EffectiveDistance = monsterData.EffectiveDistance;

			monster.GetComponent<AgentMoveToPlayer>()?.Construct(_heroGameObject.transform, _timerService);
			monster.GetComponent<RotateToHero>()?.Construct(_heroGameObject.transform);

			LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
			Debug.Log(lootSpawner);
			lootSpawner.Construct(this, _randomService);
			lootSpawner.SetLootValue(monsterData.MinLootValue, monsterData.MaxLootValue);

			Monsters.Add(monster);
			return monster;
		}

		public GameObject CreateMonster(MonsterTypeId typeId, Transform parent, float progressMultiplier)
		{
			MonsterStaticData monsterData = _staticData.ForMonster(typeId);
			GameObject monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity);

			IHealth health = monster.GetComponent<IHealth>();
			health.Current = monsterData.Hp;
			health.Max = monsterData.Hp;

			monster.GetComponent<ActorUI>()?.Construct(health, _persistentProgressService);
			monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

			Attack attack = monster.GetComponentInChildren<Attack>();
			attack.Construct(_heroGameObject.transform);
			attack.Damage = monsterData.Damage * progressMultiplier;
			attack.Cleavage = monsterData.Cleavage;
			attack.EffectiveDistance = monsterData.EffectiveDistance;

			monster.GetComponent<AgentMoveToPlayer>()?.Construct(_heroGameObject.transform, _timerService);
			monster.GetComponent<RotateToHero>()?.Construct(_heroGameObject.transform);

			LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
			lootSpawner.Construct(this, _randomService);
			lootSpawner.SetLootValue(monsterData.MinLootValue, monsterData.MaxLootValue);

			Monsters.Add(monster);
			return monster;
		}

		public void CreateSpawner(string spawnerId, Vector3 at, MonsterTypeId monsterTypeId, Transform parent)
		{
			SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, at).GetComponent<SpawnPoint>();
			spawner.transform.parent = parent;
			spawner.Construct(this, _timerService);
			spawner.MonsterTypeId = monsterTypeId;
			spawner.Id = spawnerId;
			Spawners.Add(spawner);
		}

		public void CreateBossSpawner(Vector3 at, MonsterTypeId monsterTypeId, Transform parent)
		{
			_bossSpawner = InstantiateRegistered(AssetPath.BossSpawner, at).GetComponent<BossSpawnPoint>();
			_bossSpawner.transform.parent = parent;
			_bossSpawner.Construct(this, _timerService);
			_bossSpawner.MonsterTypeId = monsterTypeId;
		}

		public void Dispose()
		{
			Object.Destroy(_heroGameObject);
			foreach (GameObject monster in Monsters)
			{
				Object.Destroy(monster.gameObject);
			}
		}

		private void Register(ISavedProgressReader progressReader)
		{
			if (progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);

			ProgressReaders.Add(progressReader);
		}

		public void Cleanup()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();
		}

		private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
		{
			GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);
			RegisterProgressWatchers(gameObject);

			return gameObject;
		}

		private GameObject InstantiateRegistered(string prefabPath)
		{
			GameObject gameObject = _assets.Instantiate(path: prefabPath);
			RegisterProgressWatchers(gameObject);

			return gameObject;
		}

		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
				Register(progressReader);
		}

	}
}