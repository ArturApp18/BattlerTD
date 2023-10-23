using System;
using UnityEngine;

namespace Counters
{
	public class StoveCounterVisual : MonoBehaviour
	{
		[SerializeField] private StoveCounter _stoveCounter;
		[SerializeField] private GameObject _particleSystem;
		[SerializeField] private GameObject _stoveVisual;

		private void Start()
		{
			_stoveCounter.OnStateChanged += StoveCounterOnStateChanged;
		}

		private void StoveCounterOnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
		{
			bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
			_stoveVisual.SetActive(showVisual);
			_particleSystem.SetActive(showVisual);
		}
	}
}