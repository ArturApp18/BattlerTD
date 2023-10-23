using System;
using UnityEngine;

namespace CodeBase.Logic
{
	[RequireComponent(typeof(Collider))]
	public class TriggerObserver : MonoBehaviour
	{
		public event Action<Collider> TriggerEnter;
		public event Action<Collider> TriggerStay;
		public event Action<Collider> TriggerExit;

		private void OnTriggerEnter(Collider other)
		{
			TriggerEnter?.Invoke(other);
		}

		private void OnTriggerStay(Collider other)
		{
			TriggerStay?.Invoke(other);
		}

		private void OnTriggerExit(Collider other)
		{
			TriggerExit?.Invoke(other);
		}
	}
}