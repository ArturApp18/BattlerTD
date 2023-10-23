using System;
using System.Collections.Generic;
using StaticData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Counters
{
	public class PlateKitchenObject : KitchenObject
	{
		public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

		public class OnIngredientAddedEventArgs : EventArgs
		{
			public KitchenProductsData kitchenProductsData;
		}

		[SerializeField] private List<KitchenProductsData> _validKitchenProductsData;

		private List<KitchenProductsData> _kitchenProductsData;
		

		private void Awake()
		{
			_kitchenProductsData = new List<KitchenProductsData>();
		}

		public bool TryAddIngredient(KitchenProductsData kitchenProductsData)
		{
			if (!_validKitchenProductsData.Contains(kitchenProductsData))
			{
				return false;
			}

			if (_kitchenProductsData.Contains(kitchenProductsData))
			{
				return false;
			}
			else
			{
				_kitchenProductsData.Add(kitchenProductsData);

				OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
					kitchenProductsData = kitchenProductsData,
				});
				return true;
			}
		}

		public List<KitchenProductsData> GetKitchenProductsDataList()
		{
			return _kitchenProductsData;
		}
	}
}