using CodeBase.Infrastructure.Services.Timers;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
	public class GamePlayingClockUI : MonoBehaviour
	{
		[SerializeField] private Image _timerImage;
		private ITimerService _timerService;

		public void Construct(ITimerService timerService)
		{
			_timerService = timerService;
		}

		public void RestartTimer()
		{
			_timerImage.fillAmount = 1;
		}

		private void Update()
		{
			_timerImage.fillAmount = _timerService.GetPlayingTimerNormalized();
		}
	}

}