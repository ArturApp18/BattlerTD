using System;
using Infrastructure;
using Services.Input;
using UnityEngine;

namespace Logic
{
	public class GameLoop : MonoBehaviour
	{
		public static GameLoop Instance { get; private set; }

		public event EventHandler OnStateChanged;
		public event EventHandler OnGamePaused;
		public event EventHandler OnGameUnpaused;

		private IInputService _inputService;
		private enum State
		{
			WaitingToStart,
			CountdownToStart,
			GamePlaying,
			GameOver,
		}

		private State _state;
		
		private float _waitingStartTimer = 1;
		private float _countdownStartTimer = 3;
		private float _gamePlayTimer;
		private float _gamePlayTimerMax = 300;
		private bool _isGamePaused = false;

		private void Awake()
		{
			Instance = this;
			_state = State.WaitingToStart;
			_inputService = Game.InputService;
		}

		private void Update()
		{
			switch (_state)
			{
				case State.WaitingToStart:
					_waitingStartTimer -= Time.deltaTime;
					if (_waitingStartTimer < 0)
					{
						_state = State.CountdownToStart;
						OnStateChanged?.Invoke(this, EventArgs.Empty);
					}

					break;
				case State.CountdownToStart:
					_countdownStartTimer -= Time.deltaTime;
					if (_countdownStartTimer < 0)
					{
						_state = State.GamePlaying;
						_gamePlayTimer = _gamePlayTimerMax;
						OnStateChanged?.Invoke(this, EventArgs.Empty);
					}

					break;
				case State.GamePlaying:
					_gamePlayTimer -= Time.deltaTime;
					if (_gamePlayTimer < 0)
					{
						_state = State.GameOver;
						OnStateChanged?.Invoke(this, EventArgs.Empty);
					}

					break;
				case State.GameOver:
					break;
			}

			Debug.Log(_state);

			if (_inputService.IsPauseButtonDown())
				TogglePauseGame();

		}

		public bool IsPlayerGame() =>
			_state == State.GamePlaying;

		public bool IsCountdownToStartActive() =>
			_state == State.CountdownToStart;

		public void TogglePauseGame()
		{
			_isGamePaused = !_isGamePaused;

			if (_isGamePaused)
			{
				Time.timeScale = 0f;
				OnGamePaused?.Invoke(this, EventArgs.Empty);
			}
			else
			{
				Time.timeScale = 1;
				
				OnGameUnpaused?.Invoke(this, EventArgs.Empty);
			}
		}

		public float GetCountdownToStartTimer() =>
			_countdownStartTimer;

		public bool IsGameOver() =>
			_state == State.GameOver;

		public float GetPlayingTimerNormalized() =>
			1 - (_gamePlayTimer / _gamePlayTimerMax);

	}

}