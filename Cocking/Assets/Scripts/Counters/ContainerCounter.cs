using System;
using Hero;
using Logic;
using StaticData;
using UnityEngine;

namespace Counters
{
	public class ContainerCounter : BaseCounter
	{
		[SerializeField] private KitchenProductsData _kitchenProductsData;

		public event EventHandler OnPlayerGrabbedObject;

		public override void Interact(HeroInteractions heroInteraction)
		{
			if (!heroInteraction.HasKitchenObject())
			{
				InstantiationKitchenObject(_kitchenProductsData, heroInteraction);
				
				OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
			}
		}
	}

}