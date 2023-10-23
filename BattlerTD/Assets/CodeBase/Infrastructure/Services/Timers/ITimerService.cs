using System;

namespace CodeBase.Infrastructure.Services.Timers
{
	public interface ITimerService : IService, IDisposable
	{
		float Timer { get; set; }

		bool IsTimerActive { get; set; }

		float TotalTime { get; set; }
		float LevelStage { get; set; }

		float GetPlayingTimerNormalized();
		void StopTimer();
		void StartTimer();

		event Action FirstStagePassed;
		event Action SecondStagePassed;
		event Action ThirdStagePassed;
		event Action FourthStagePassed;
		event Action FifthStagePassed;
	}
}