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
		private ITimerService _timerService;

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

		public void Construct(IGameFactory gameFactory, ITimerService timerService)
		{
			_factory = gameFactory;
			_timerService = timerService;
		}

		private void Start()
		{
			_timerService.FirstStagePassed += FirstStagePassed;
			_timerService.SecondStagePassed += SecondStagePassed;
			IsActive = true;
		}

		private void SecondStagePassed()
		{
			StartCoroutine(RangeMob());
		}

		private void FirstStagePassed()
		{
			StartCoroutine(MeleeMob());
		}

		private void OnDestroy()
		{
			IsActive = false;
			if (_enemyDeath != null)
				_enemyDeath.Happened -= Slay;
		}

		public void LoadProgress(PlayerProgress progress)
		{
			if (progress.KillData.ClearedSpawners.Contains(Id))
				_slain = true;
			else
				StartCoroutine(NormalMob());
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			List<string> slainSpawnersList = progress.KillData.ClearedSpawners;

			if (_slain && !slainSpawnersList.Contains(Id))
				slainSpawnersList.Add(Id);
		}

		private IEnumerator NormalMob()
		{
			while (IsActive)
			{
				GameObject monster = _factory.CreateMonster(MonsterTypeId, transform, _timerService.LevelStage);
				_enemyDeath = monster.GetComponent<EnemyDeath>();
				_enemyDeath.Happened += Slay;
				yield return new WaitForSeconds(_delayBetweenSpawn);
			}
		}		
		
		private IEnumerator MeleeMob()
		{
			while (IsActive)
			{
				GameObject monster = _factory.CreateMonster(MonsterTypeId.Orc, transform, _timerService.LevelStage);
				_enemyDeath = monster.GetComponent<EnemyDeath>();
				_enemyDeath.Happened += Slay;
				yield return new WaitForSeconds(_delayBetweenSpawn);
			}
		}		
		
		private IEnumerator RangeMob()
		{
			while (IsActive)
			{
				GameObject monster = _factory.CreateMonster(MonsterTypeId.Wizard, transform, _timerService.LevelStage);
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