using System;
using Hero;
using StaticData;
using UI;
using UnityEngine;

namespace Counters
{
	public class CuttingCounter : BaseCounter, IHasProgress
	{
		public static event EventHandler OnAnyCut; 

		public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
		public event EventHandler OnCut;

		[SerializeField] private CuttingRecipeData[] _cuttingRecipeDatas;

		private float _cuttingProgress;

		public override void Interact(HeroInteractions heroInteraction)
		{
			if (!HasKitchenObject())
			{
				if (heroInteraction.HasKitchenObject())
				{
					if (HasRecipeWithInput(heroInteraction.GetKitchenObject().GetKitchenProductsData()))
					{
						heroInteraction.GetKitchenObject().SetKitchenObjectParent(this);

						_cuttingProgress = 0;

						CuttingRecipeData cuttingRecipeData = GetCuttingRecipeDataWithInput(GetKitchenObject().GetKitchenProductsData());

						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
							ProgressNormalized = _cuttingProgress / cuttingRecipeData.CuttingProgressMax,
						});
					}
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
				}
				else
				{
					GetKitchenObject().SetKitchenObjectParent(heroInteraction);
				}
			}
		}

		public override void InteractAlternate(HeroInteractions interaction)
		{
			if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenProductsData()))
			{
				_cuttingProgress++;

				OnCut?.Invoke(this, EventArgs.Empty);
				OnAnyCut?.Invoke(this, EventArgs.Empty);
				CuttingRecipeData cuttingRecipeData = GetCuttingRecipeDataWithInput(GetKitchenObject().GetKitchenProductsData());

				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
					ProgressNormalized = _cuttingProgress / cuttingRecipeData.CuttingProgressMax,
				});

				if (_cuttingProgress >= cuttingRecipeData.CuttingProgressMax)
				{
					KitchenProductsData outputKitchenProductsData = GetOutputForInput(GetKitchenObject().GetKitchenProductsData());
					GetKitchenObject().DestroySelf();

					InstantiationKitchenObject(outputKitchenProductsData, this);
				}
			}
		}

		private bool HasRecipeWithInput(KitchenProductsData inputKitchenProductsData)
		{
			CuttingRecipeData cuttingRecipeData = GetCuttingRecipeDataWithInput(inputKitchenProductsData);
			return cuttingRecipeData != null;
		}

		private KitchenProductsData GetOutputForInput(KitchenProductsData inputKitchenProductsData)
		{
			CuttingRecipeData cuttingRecipeData = GetCuttingRecipeDataWithInput(inputKitchenProductsData);

			if (cuttingRecipeData != null)
				return cuttingRecipeData.Output;
			else
				return null;
		}

		private CuttingRecipeData GetCuttingRecipeDataWithInput(KitchenProductsData inputKitchenProductsData)
		{
			foreach (CuttingRecipeData cuttingRecipeData in _cuttingRecipeDatas)
				if (cuttingRecipeData.Input == inputKitchenProductsData)
					return cuttingRecipeData;

			return null;
		}
	}
}