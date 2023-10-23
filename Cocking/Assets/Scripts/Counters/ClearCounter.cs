using Hero;
using StaticData;
using UnityEngine;

namespace Counters
{
	public class ClearCounter : BaseCounter
	{
		[SerializeField] private KitchenProductsData _kitchenProductsData;


		public override void Interact(HeroInteractions heroInteraction)
		{
			if (!HasKitchenObject())
			{
				if (heroInteraction.HasKitchenObject())
				{
					heroInteraction.GetKitchenObject().SetKitchenObjectParent(this);
				}
				else { }
			}
			else
			{
				if (heroInteraction.HasKitchenObject())
				{
					if (heroInteraction.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
					{
						if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenProductsData()))
						{
							GetKitchenObject().DestroySelf();
						}
					}
					else
					{
						if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
						{
							if (plateKitchenObject.TryAddIngredient(heroInteraction.GetKitchenObject().GetKitchenProductsData()))
							{
								heroInteraction.GetKitchenObject().DestroySelf();
							}
						}
					}
				}
				else
				{
					GetKitchenObject().SetKitchenObjectParent(heroInteraction);
				}
			}
		}

	}

}