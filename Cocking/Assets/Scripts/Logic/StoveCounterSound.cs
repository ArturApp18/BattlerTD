using System;
using Counters;
using UnityEngine;

namespace Logic
{
	public class StoveCounterSound : MonoBehaviour
	{
		[SerializeField] private StoveCounter _stoveCounter;
		[SerializeField] private AudioSource _audioSource;

		private void Start()
		{
			_stoveCounter.OnStateChanged += StoveCounterOnStateChanged;
		}

		private void StoveCounterOnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
		{
			bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

			if (playSound)
			{
				_audioSource.Play();
			}
			else
			{
				_audioSource.Pause();
			}
		}
	}

}