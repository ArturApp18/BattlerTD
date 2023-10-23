using System;
using Counters;
using Infrastructure;
using Logic;
using Services.Input;
using UnityEngine;

namespace Hero
{
	public class HeroInteractions : MonoBehaviour, IKitchenObjectParent
	{
		public static HeroInteractions Instance { get; private set; }

		public static event EventHandler OnPicked;

		[SerializeField] private BaseCounter _selecetedCounter;
		[SerializeField] private Transform _kitchenObjectHoldPoint;

		private IInputService _inputService;
		private KitchenObject _kitchenObject;


		private void Awake()
		{
			Instance = this;
			_inputService = Game.InputService;
		}

		private void Update()
		{
			if (!GameLoop.Instance.IsPlayerGame())
				return;

			if (_selecetedCounter != null && _inputService.IsActionButtonDown())
			{
				_selecetedCounter.Interact(this);
			}
			
			if (_selecetedCounter != null && _inputService.IsAlternativeActionButtonDown())
			{
				_selecetedCounter.InteractAlternate(this);
			}
		}

		public void GetSelectedCounter(BaseCounter clearCounter)
		{
			_selecetedCounter = clearCounter;
		}

		public Transform GetKitchenObjectFollowTransform() =>
			_kitchenObjectHoldPoint;

		public void SetKitchenObject(KitchenObject kitchenObject)
		{
			_kitchenObject = kitchenObject;

			if (kitchenObject != null)
			{
				OnPicked?.Invoke(this, EventArgs.Empty);
			}
		}

		public KitchenObject GetKitchenObject() =>
			_kitchenObject;

		public void ClearKitchenObject() =>
			_kitchenObject = null;

		public bool HasKitchenObject() =>
			_kitchenObject != null;
	}
}