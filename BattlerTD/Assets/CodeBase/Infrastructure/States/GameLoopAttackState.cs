using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
	public class GameLoopAttackState : IState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IWindowService _windowService;
		private readonly IPersistentProgressService _progressService;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly ITimerService _timerService;
		private readonly IGameFactory _gameFactory;
		private readonly SceneLoader _sceneLoader;
		private IHealth _heroHealth;
		private float _monstersForWave = 2;
		private List<GameObject> _monstersInGame = new List<GameObject>();

		public GameLoopAttackState(GameStateMachine stateMachine, IWindowService windowService, IPersistentProgressService progressService, LoadingCurtain loadingCurtain,
			ITimerService timerService, IGameFactory gameFactory)
		{
			_stateMachine = stateMachine;
			_windowService = windowService;
			_progressService = progressService;
			_loadingCurtain = loadingCurtain;
			_timerService = timerService;
			_gameFactory = gameFactory;
		}

		public void Exit()
		{
			DescribeHeroDeath();
			StopWave();
			_gameFactory.HUD.DisappearAttackButton();
			_monstersForWave += 7;
			_gameFactory.Monsters.Clear();
			_progressService.Progress.KillData.ResetKillData();
			_gameFactory.MonsterCreated -= MonsterCreated;
			_progressService.Progress.KillData.Changed -= KilledMobsChanged;
		}

		public void Enter()
		{
			_gameFactory.HUD.AppearAttackButton();
			_gameFactory.MonsterCreated += MonsterCreated;
			_progressService.Progress.KillData.Changed += KilledMobsChanged;
			_gameFactory.HeroGameObject.TryGetComponent(out _heroHealth);
			SubscribeHeroDeath();

			StartWave();

			_progressService.Progress.WorldData.LootData.LevelUp += LevelUp;
			Hid();
		}

		public void Update()
		{

		}

		private void DescribeHeroDeath()
		{
			_gameFactory.HeroGameObject.TryGetComponent(out HeroDeath heroDeath);
			heroDeath.Restart -= Restart;
			_gameFactory.MainPumpkinGameObject.TryGetComponent(out BuildingHealth health);
			health.Restart -= Restart;
		}


		private void MonsterCreated(GameObject monster)
		{
			_monstersInGame.Add(monster);
			if (_monstersInGame.Count >= _monstersForWave)
				StopWave();
		}

		private void KilledMobsChanged()
		{
			if (_monstersInGame.Count > 0)
				_monstersInGame.RemoveAt(_monstersInGame.Count - 1);

			if (_progressService.Progress.KillData.KilledMobs >= _monstersForWave && _monstersInGame.Count < 1)
				_stateMachine.Enter<GameLoopBuildingState>();
		}

		private void StartWave()
		{
			float result = _monstersForWave / _gameFactory.Spawners.Count;
			
			foreach (SpawnPoint spawner in _gameFactory.Spawners)
				spawner.StartSpawn(result);
		}

		private void StopWave()
		{
			foreach (SpawnPoint spawner in _gameFactory.Spawners)
				spawner.StopSpawn(_monstersForWave);
		}

		private void OnCutsceneEnded() { }

		private void SubscribeHeroDeath()
		{
			_gameFactory.HeroGameObject.TryGetComponent(out HeroDeath heroDeath);
			heroDeath.Restart += Restart;
			_gameFactory.MainPumpkinGameObject.TryGetComponent(out BuildingHealth health);
			health.Restart += Restart;
		}


		private void Restart()
		{
			_loadingCurtain.Show();

			_stateMachine.Enter<RestartLevelState>();
		}

		private void Hid()
		{
			_timerService.StopTimer();
		}
		
		private void LevelUp()
		{
			_heroHealth.LevelUp();
			Hid();
			_timerService.LevelStage *= 1.15f;
		}
	}

}