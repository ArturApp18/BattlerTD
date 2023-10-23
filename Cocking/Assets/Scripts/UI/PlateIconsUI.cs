using System;
using Counters;
using StaticData;
using UnityEngine;

namespace UI
{
	public class PlateIconsUI : MonoBehaviour
	{
		[SerializeField] private PlateKitchenObject _plateKitchenObject;
		[SerializeField] private Transform _iconTemplate;

		private void Awake()
		{
			_iconTemplate.gameObject.SetActive(false);
		}

		private void Start()
		{
			_plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnIngredientAdded;
		}

		private void OnDestroy()
		{
			_plateKitchenObject.OnIngredientAdded -= PlateKitchenObjectOnIngredientAdded;
		}

		private void PlateKitchenObjectOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
		{
			UpdateVisual();
		}

		private void UpdateVisual()
		{
			foreach (Transform child in transform)
			{
				if (child == _iconTemplate)
					continue;

				Destroy(child.gameObject);
			}

			foreach (KitchenProductsData kitchenProductsData in _plateKitchenObject.GetKitchenProductsDataList())
			{
				Transform iconTransform = Instantiate(_iconTemplate, transform);
				iconTransform.gameObject.SetActive(true);
				iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectData(kitchenProductsData);
			}
		}
	}

}