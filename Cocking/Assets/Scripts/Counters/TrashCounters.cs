using System;
using Hero;

namespace Counters
{
	public class TrashCounters : BaseCounter
	{

		public static event EventHandler OnAnyObjectTrashed;
		public override void Interact(HeroInteractions heroInteraction)
		{
			if (heroInteraction.HasKitchenObject())
			{
				heroInteraction.GetKitchenObject().DestroySelf();
				
				OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
			}
		}
	}

}