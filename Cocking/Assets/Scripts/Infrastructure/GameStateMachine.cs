using System;
using System.Collections.Generic;

namespace Infrastructure
{
	public class GameStateMachine
	{
		private Dictionary<Type, IExitableState> _states;
		private IExitableState _activeState;

		public GameStateMachine(SceneLoader sceneLoader)
		{
			_states = new Dictionary<Type, IExitableState> 
			{
				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
				[typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
			};
		}
		
		public void Enter<TState>() where TState : class, IState
		{
			TState state = ChangeState<TState>(); 
			state?.Enter();
		}

		public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
		{
			TState state = ChangeState<TState>();
			state.Enter(payload);
		}

		public void Update()
		{
			
		}

		private TState GetState<TState>() where TState : class, IExitableState =>
			_states[typeof(TState)] as TState;
		
		

		private TState ChangeState<TState>() where TState : class, IExitableState
		{
			_activeState?.Exit();
			
			TState state = GetState<TState>();
			_activeState = state;
			
			return state;
		}
	}

}