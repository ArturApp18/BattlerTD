using System;
using System.Collections.Generic;
using StaticData;
using UnityEngine;

namespace Counters
{
	public class PlateCompleteVisual : MonoBehaviour
	{
		[Serializable]
		public struct KitchenProductDataGameObject
		{
			public KitchenProductsData KitchenProductsData;
			public GameObject GameObject;
		}

		[SerializeField] private PlateKitchenObject _plateKitchenObject;
		[SerializeField] private List<KitchenProductDataGameObject> _kitchenProductDataGameObjects;

		private void Start()
		{
			_plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnIngredientAdded;

			foreach (KitchenProductDataGameObject kitchenProductDataGameObject in _kitchenProductDataGameObjects)
			{
				kitchenProductDataGameObject.GameObject.SetActive(false);
				Debug.Log("Start");
			}
		}

		private void PlateKitchenObjectOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs  e)
		{
			foreach (KitchenProductDataGameObject kitchenProductDataGameObject in _kitchenProductDataGameObjects)
			{
				if (kitchenProductDataGameObject.KitchenProductsData == e.kitchenProductsData)
				{
					kitchenProductDataGameObject.GameObject.SetActive(true);
				}
				Debug.Log("Finish");
			}
		}
	}
}