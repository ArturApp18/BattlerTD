using System;
using System.Collections.Generic;
using StaticData;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Counters.CountersLogic
{
	public class OrderReceiver : MonoBehaviour
	{
		public event EventHandler OnRecipeSpawned;
		public event EventHandler OnRecipeCompleted; 
		public event EventHandler OnRecipeSuccess; 
		public event EventHandler OnRecipeFailed; 

		[SerializeField] private WaitingRecipeData _recipeData;

		private List<RecipeData> _waitingRecipeData;
		private float _spawnRecipeTimer;
		private float _spawnRecipeTimerMax = 4f;
		private int _waitingRecipeMax = 4;
		private int _successfulRecipesAmount;

		private void Awake() =>
			_waitingRecipeData = new List<RecipeData>();

		private void Update()
		{
			_spawnRecipeTimer -= Time.deltaTime;
			if (_spawnRecipeTimer <= 0f)
			{
				_spawnRecipeTimer = _spawnRecipeTimerMax;

				if (_waitingRecipeData.Count < _waitingRecipeMax)
				{
					RecipeData waitingRecipeData = _recipeData.waitingRecipeData[Random.Range(0, _recipeData.waitingRecipeData.Count)];
					
					_waitingRecipeData.Add(waitingRecipeData);
					
					OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
		{
			for (int i = 0; i < _waitingRecipeData.Count; i++)
			{
				RecipeData waitingRecipeData = _waitingRecipeData[i];

				if (waitingRecipeData.KitchenProductsData.Count == plateKitchenObject.GetKitchenProductsDataList().Count)
				{
					bool plateContentsMatchesRecipe = true;
					foreach (KitchenProductsData recipeKitchenProductsData in waitingRecipeData.KitchenProductsData)
					{
						bool ingredientFound = false;
						foreach (KitchenProductsData plateKitchenProductsData in plateKitchenObject.GetKitchenProductsDataList())
						{
							if (plateKitchenProductsData == recipeKitchenProductsData)
							{
								ingredientFound = true;
								break;
							}
						}

						if (!ingredientFound)
						{
							plateContentsMatchesRecipe = false;
						}
					}

					if (plateContentsMatchesRecipe)
					{
						_successfulRecipesAmount++;
						_waitingRecipeData.RemoveAt(i);
						
						OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
						OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
						return;
					}
				}
			}
			OnRecipeFailed?.Invoke(this, EventArgs.Empty);
		}

		public List<RecipeData> GetWaitingRecipeDataList() =>
			_waitingRecipeData;

		public int GetSuccessfulAmount() =>
			_successfulRecipesAmount;
	}
}