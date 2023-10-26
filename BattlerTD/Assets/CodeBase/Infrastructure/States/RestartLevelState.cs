using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
	public class RestartLevelState : IState
	{
		private const string Initial = "Initial";
		
		private readonly IGameFactory _gameFactory;
		private readonly ITimerService _timerService;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;

		public RestartLevelState(IGameFactory gameFactory, ITimerService timerService, LoadingCurtain loadingCurtain, IGameStateMachine stateMachine, SceneLoader sceneLoader)
		{
			_gameFactory = gameFactory;
			_timerService = timerService;
			_loadingCurtain = loadingCurtain;
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
		}

		public void Enter()
		{
			_gameFactory.Dispose();
			_timerService.Dispose();
			_sceneLoader.Load(Initial, OnLoaded);
		}

		private void OnLoaded()
		{
			_stateMachine.Enter<LoadProgressState>();
		}

		public void Exit()
		{
	
		}

		public void Update() { }

	}
}