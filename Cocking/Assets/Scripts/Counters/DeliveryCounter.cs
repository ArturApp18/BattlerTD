 using Counters.CountersLogic;
 using Hero;
 using UnityEngine;

 namespace Counters
{
	public class DeliveryCounter : BaseCounter
	{
		[SerializeField] private OrderReceiver _orderReceiver;
		public override void Interact(HeroInteractions heroInteraction)
		{
			if (heroInteraction.HasKitchenObject())
			{
				if (heroInteraction.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
				{
					_orderReceiver.DeliverRecipe(plateKitchenObject);
					heroInteraction.GetKitchenObject().DestroySelf();
				}
			}
		}
	}
}