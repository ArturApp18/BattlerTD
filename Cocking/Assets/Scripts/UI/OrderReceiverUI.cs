using System;
using Counters;
using Counters.CountersLogic;
using StaticData;
using UnityEngine;

namespace UI
{
	public class OrderReceiverUI : MonoBehaviour
	{
		[SerializeField] private Transform _container;
		[SerializeField] private Transform _recipeTemplate;
		[SerializeField] private OrderReceiver _orderReceiver;

		private void Awake()
		{
			_recipeTemplate.gameObject.SetActive(false);
		}

		private void Start()
		{
			_orderReceiver.OnRecipeSpawned += OrderReceiverOnRecipeSpawned;
			_orderReceiver.OnRecipeCompleted += OrderReceiverOnRecipeCompleted;
			
			UpdateVisual();
		}

		private void OrderReceiverOnRecipeCompleted(object sender, EventArgs e)
		{
			UpdateVisual();
		}

		private void OrderReceiverOnRecipeSpawned(object sender, EventArgs e)
		{
			UpdateVisual();
		}

		private void UpdateVisual()
		{
			foreach (Transform child in _container)
			{
				if (child == _recipeTemplate)
					continue;
				
				Destroy(child.gameObject);
			}

			foreach (RecipeData recipeData in _orderReceiver.GetWaitingRecipeDataList())
			{
				Transform recipeTransform = Instantiate(_recipeTemplate, _container);
				recipeTransform.gameObject.SetActive(true);
				recipeTransform.GetComponent<OrderReceiverSingleUI>().SetRecipeData(recipeData);
			}
		}
	}

}