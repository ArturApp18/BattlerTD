using StaticData;
using UnityEngine;

namespace Counters
{
	public class KitchenObject : MonoBehaviour
	{
		[SerializeField] private KitchenProductsData kitchenProductsData;
		
		private IKitchenObjectParent _kitchenObjectParent;

		public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
		{
			if (_kitchenObjectParent != null)
			{
				_kitchenObjectParent.ClearKitchenObject();
			}
			
			_kitchenObjectParent = kitchenObjectParent;
			if (kitchenObjectParent.HasKitchenObject())
			{
				Debug.LogError("Counter already has a KitchenObject");
			}
			kitchenObjectParent.SetKitchenObject(this);
			
			transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
			transform.localPosition = Vector3.zero;
		}

		public KitchenProductsData GetKitchenProductsData() =>
			kitchenProductsData;

		public IKitchenObjectParent GetKitchenObjectParent() =>
			_kitchenObjectParent;

		public void DestroySelf()
		{
			_kitchenObjectParent.ClearKitchenObject();

			Destroy(gameObject);
		}

		public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
		{
			if (this is PlateKitchenObject)
			{
				plateKitchenObject = this as PlateKitchenObject;
				return true;
			}
			else
			{
				plateKitchenObject = null;
				return false;
			}
		}
	}
}