using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{

	public class SpawnPoint : MonoBehaviour, ISavedProgress
	{
		[SerializeField] private float _delayBetweenSpawn = 7;
		public MonsterTypeId MonsterTypeId;
		
		public string Id { get; set; }

		private IGameFactory _factory;

		private EnemyDeath _enemyDeath;

		private bool _slain;
		public bool _isActive;
		private float _levelStage = 1;
		public bool IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
			}
		}

		public void Construct(IGameFactory gameFactory)
		{
			_factory = gameFactory;
		}

		private void Start()
		{
			IsActive = true;
		}

		private void OnDestroy()
		{
			IsActive = false;
			if (_enemyDeath != null)
				_enemyDeath.Happened -= Slay;
		}

		public void StartSpawn(float times) =>
			StartCoroutine(SpawnNormalMob(times));		
		
		public void StopSpawn(float times) =>
			StopCoroutine(SpawnNormalMob(times));

		public void LoadProgress(PlayerProgress progress)
		{
			
		}

		public void UpdateProgress(PlayerProgress progress)
		{

		}

		private IEnumerator SpawnNormalMob(float times)
		{
			for (int i = 0; i < times; i++)
			{
				GameObject monster = _factory.CreateMonster(MonsterTypeId, transform);
				_enemyDeath = monster.GetComponent<EnemyDeath>();
				_enemyDeath.Happened += Slay;
				yield return new WaitForSeconds(_delayBetweenSpawn);
			}
		}		
		
		private void Slay()
		{
			if (_enemyDeath != null)
				_enemyDeath.Happened -= Slay;

			_slain = true;
		}
	}
}