using System;
using Hero;
using Logic;
using StaticData;
using UnityEngine;

namespace Counters
{
	public class BaseCounter : MonoBehaviour, IKitchenObjectParent
	{
		public static event EventHandler OnAnyObjectPlaced;
		
		[SerializeField] private Transform _counterTopPoint;
		[SerializeField] private HeroInteractions _interactions;
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private KitchenObject kitchenObject;

		private void Awake()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;
			_triggerObserver.TriggerStay += TriggerStay;
		}

		private void OnDestroy()
		{
			_triggerObserver.TriggerEnter -= TriggerEnter;
			_triggerObserver.TriggerExit -= TriggerExit;
			_triggerObserver.TriggerStay -= TriggerStay;
		}

		public virtual void Interact(HeroInteractions heroInteraction)
		{
			Debug.LogError("BaseCounter.Interact()");
		}

		public virtual void InteractAlternate(HeroInteractions interaction)
		{
			Debug.LogError("BaseCounter.Interact()");
		}

		protected KitchenObject InstantiationKitchenObject(KitchenProductsData kitchenProductsData, IKitchenObjectParent kitchenObjectParent)
		{
			Transform kitchenObjectTransform = Instantiate(kitchenProductsData.Prefab);

			KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
			
			kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

			return kitchenObject;
		}

		private void TriggerStay(Collider obj) { }

		private void TriggerExit(Collider obj)
		{
			_interactions.GetSelectedCounter(null);
		}

		private void TriggerEnter(Collider obj)
		{
			_interactions = obj.GetComponent<HeroInteractions>();
			_interactions.GetSelectedCounter(this);
		}

		public Transform GetKitchenObjectFollowTransform() =>
			_counterTopPoint;

		public void SetKitchenObject(KitchenObject kitchenObject)
		{
			this.kitchenObject = kitchenObject;

			if (kitchenObject != null)
				OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
		}

		public KitchenObject GetKitchenObject() =>
			kitchenObject;

		public void ClearKitchenObject() =>
			kitchenObject = null;

		public bool HasKitchenObject() =>
			kitchenObject != null;
	}

}