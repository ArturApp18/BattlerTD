using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Timers
{
	public class TimerService : ITimerService
	{
		public event Action FirstStagePassed;
		public event Action SecondStagePassed;
		public event Action ThirdStagePassed;
		public event Action FourthStagePassed;
		public event Action FifthStagePassed;

		private float _timer;
		public float Timer
		{
			get
			{
				return _timer;
			}
			set
			{
				_timer = value;
				CheckStage();
			}
		}

		public float LevelStage { get; set; } = 1;

		public float TotalTime { get; set; } = 180;

		public bool IsTimerActive { get; set; }

		private bool _isFirstStagePassed;

		private bool _isSecondStagePassed;

		private bool _isThirdStagePassed;

		private bool _isFourthStagePassed;

		private bool _isFifthStagePassed;

		public float GetPlayingTimerNormalized() =>
			1 - ( Timer / TotalTime );

		public void StopTimer()
		{
			IsTimerActive = false;
		}

		public void StartTimer()
		{
			IsTimerActive = true;
		}

		private void FirstStagePassedEvent()
		{
			_isFirstStagePassed = true;
			FirstStagePassed?.Invoke();
		}


		private void SecondStagePassedEvent()
		{
			_isSecondStagePassed = true;
			SecondStagePassed?.Invoke();
		}


		private void ThirdStagePassedEvent()
		{
			_isThirdStagePassed = true;
			ThirdStagePassed?.Invoke();
		}

		private void FourthStagePassedEvent()
		{
			_isFourthStagePassed = true;
			FourthStagePassed?.Invoke();
		}

		private void FifthStagePassedEvent()
		{
			_isFifthStagePassed = true;
			FifthStagePassed?.Invoke();
		}

		public void Dispose()
		{
			RestartTimer();
		}

		private void RestartTimer()
		{
			Timer = 0;
		}

		private void CheckStage()
		{
			if (Timer >= TotalTime * 0.1f && !_isFirstStagePassed)
			{
				Debug.Log($"{Timer} { ( 10 / 100 )}" );
				FirstStagePassedEvent();
			}
			else if (Timer >= TotalTime * 0.3f && !_isSecondStagePassed)
			{
				_isSecondStagePassed = true;
				SecondStagePassedEvent();
			}
			else if (Timer >= TotalTime * 0.5f && !_isThirdStagePassed)
			{
				_isThirdStagePassed = true;
				ThirdStagePassedEvent();
			}
			else if (Timer >= TotalTime * 0.6f && !_isFourthStagePassed)
			{
				_isFourthStagePassed = true;
				FourthStagePassedEvent();
			}
			else if (Timer >= TotalTime * 0.75f && !_isFifthStagePassed)
			{
				_isFifthStagePassed = true;
				FifthStagePassedEvent();
			}
		}
	}
}