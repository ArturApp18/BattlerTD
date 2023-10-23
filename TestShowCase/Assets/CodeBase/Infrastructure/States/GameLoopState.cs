using System.Collections;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.Playables;

namespace CodeBase.Infrastructure.States
{
	public class GameLoopState : IState
	{
		private const string Initial = "Initial";
		private readonly GameStateMachine _stateMachine;
		private readonly IWindowService _windowService;
		private readonly IPersistentProgressService _progressService;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly ITimerService _timerService;
		private readonly IGameFactory _gameFactory;
		private readonly SceneLoader _sceneLoader;
		private bool _bossSpawned;
		private IHealth _heroHealth;
		private bool _isFirstStagePassed;
		

		public GameLoopState(GameStateMachine stateMachine, IWindowService windowService, IPersistentProgressService progressService, LoadingCurtain loadingCurtain,
			ITimerService timerService, IGameFactory gameFactory)
		{
			_stateMachine = stateMachine;
			_windowService = windowService;
			_progressService = progressService;
			_loadingCurtain = loadingCurtain;
			_timerService = timerService;
			_gameFactory = gameFactory;
		}

		public void Exit() { }


		public void Update()
		{
			if (!_timerService.IsTimerActive)
				return;

			_timerService.Timer += Time.deltaTime;
			if (!( _timerService.Timer >= _timerService.TotalTime ) || !_timerService.IsTimerActive)
				return;
//переделай на событие дура
	
			if (!_bossSpawned)
			{
				_bossSpawned = true;
				foreach (SpawnPoint spawner in _gameFactory.Spawners)
				{
					spawner._isActive = false;
				}
				GameObject boss = _gameFactory.BossSpawner.Spawn();
			}

			if (_timerService.Timer >= _timerService.TotalTime * (15 / 100) && !_isFirstStagePassed)
			{
				_isFirstStagePassed = true;
				ImproveSpawners();
			}
			
		}

		private void ImproveSpawners()
		{
			
		}

		public void Enter()
		{
			CutsceneManager.Instance.StartCutscene("Cutscene_one");
			CutsceneManager.Instance.OnCutsceneEnded += OnCutsceneEnded;
			_bossSpawned = false;
			_heroHealth = _gameFactory.HeroGameObject.GetComponent<IHealth>();
			SubscribeHeroDeath();
			_gameFactory.BossSpawner.BossSlayed += Restart;
		
			_progressService.Progress.WorldData.LootData.LevelUp += LevelUp;
			Hid();
		}
		

		private void OnCutsceneEnded()
		{
			_windowService.Open(WindowId.SkillPanel);
		}

		private void SubscribeHeroDeath()
		{
			HeroDeath heroDeath = _gameFactory.HeroGameObject.GetComponent<HeroDeath>();
			heroDeath.Restart += Restart;
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
			_windowService.Open(WindowId.SkillPanel);
			Hid();
			_timerService.LevelStage *= 1.15f;
			_progressService.Progress.HeroStats.Damage *= 1.075f;
		}
	}

}