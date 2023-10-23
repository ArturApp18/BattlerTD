using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Tower
{
	public class CheckClosestTarget : MonoBehaviour
	{
		[SerializeField] private TriggerObserver TriggerObserver;

		private Transform _target;
		public Transform Target
		{
			get
			{
				return _target;
			}
		}

		private void Start()
		{
			TriggerObserver.TriggerEnter += TriggerEnter;
			TriggerObserver.TriggerStay += TriggerStay;
			TriggerObserver.TriggerExit += TriggerExit;
		}

		private void OnDestroy()
		{
			TriggerObserver.TriggerEnter -= TriggerEnter;
			TriggerObserver.TriggerStay -= TriggerStay;
			TriggerObserver.TriggerExit -= TriggerExit;
		}

		private void TriggerStay(Collider obj)
		{
			if (!_target)
				_target = obj.transform;
		}

		private void TriggerEnter(Collider other)
		{
			if (!_target)
				_target = other.transform;
		}

		private void TriggerExit(Collider other)
		{
			if (other.transform == Target)
				_target = null;
		}
	}
}