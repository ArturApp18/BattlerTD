using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
	public class BossSpawnPoint : MonoBehaviour
	{
		public MonsterTypeId MonsterTypeId;

		private IGameFactory _factory;
		private ITimerService _timerService;
		private EnemyDeath _enemyDeath;
		private bool _slain;

		public Action BossSlayed;
		public float _delayBeforeCallBack;

		public void Construct(IGameFactory gameFactory, ITimerService timerService)
		{
			_factory = gameFactory;
			_timerService = timerService;
		}

		public GameObject Spawn()
		{
			GameObject monster = _factory.CreateBoss(MonsterTypeId, transform, _timerService.LevelStage);
			_enemyDeath = monster.GetComponent<EnemyDeath>();
			//_enemyDeath.Happened += Slay;
			return monster;
		}

		private void Slay()
		{
			StartCoroutine(BossDefeated());
		}

		private IEnumerator BossDefeated()
		{
			yield return new WaitForSeconds(_delayBeforeCallBack);
			BossSlayed?.Invoke();
		}
	}
}