using System;
using Logic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class GamePlayingClockUI : MonoBehaviour
	{
		[SerializeField] private Image _timerImage;

		private void Update()
		{
			_timerImage.fillAmount = GameLoop.Instance.GetPlayingTimerNormalized();
		}
	}

}