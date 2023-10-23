using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Audio;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.Skills;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.Logic;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
	public class GameStateMachine : IGameStateMachine
	{
		private Dictionary<Type, IExitableState> _states;
		private IExitableState _activeState;

		public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services, AudioSource audioSource)
		{
			_states = new Dictionary<Type, IExitableState> {
				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, audioSource),
				[typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>(),
					services.Single<IPersistentProgressService>(), services.Single<IStaticDataService>(), services.Single<IUIFactory>(), services.Single<ISkillService>(),
					services.Single<ITimerService>(), services.Single<IAudioService>()),

				[typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
				[typeof(GameLoopState)] = new GameLoopState(this, services.Single<IWindowService>(), services.Single<IPersistentProgressService>(), loadingCurtain,
					services.Single<ITimerService>(), services.Single<IGameFactory>()),
				[typeof(RestartLevelState)] = new RestartLevelState(services.Single<IGameFactory>(), services.Single<ITimerService>(), loadingCurtain, this, sceneLoader),
			};
		}

		public void Enter<TState>() where TState : class, IState
		{
			IState state = ChangeState<TState>();
			state.Enter();
		}

		public void Update()
		{
			_activeState?.Update();
		}

		public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
		{
			TState state = ChangeState<TState>();
			state.Enter(payload);
		}

		private TState ChangeState<TState>() where TState : class, IExitableState
		{
			_activeState?.Exit();

			TState state = GetState<TState>();
			_activeState = state;

			return state;
		}

		private TState GetState<TState>() where TState : class, IExitableState =>
			_states[typeof(TState)] as TState;
	}
}