using System;
using System.Collections;
using Hero;
using StaticData;
using UI;
using UnityEngine;

namespace Counters
{
	public class StoveCounter : BaseCounter, IHasProgress
	{
		public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
		public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

		public class OnStateChangedEventArgs : EventArgs
		{
			public State state;
		}

		public enum State
		{
			Idle,
			Frying,
			Fried,
			Burned,
		}

		private State _state;

		[SerializeField] private FryingRecipeData[] _fryingRecipeDatas;
		[SerializeField] private BurningRecipeData[] _burningRecipeDatas;
		[SerializeField] private StoveCounterVisual _stoveCounterVisual;

		private FryingRecipeData _fryingRecipeData;
		private BurningRecipeData _burningRecipeData;

		private float _fryingTimer;
		private float _burningTimer;


		private void Start()
		{
			_state = State.Idle;
		}

		private void Update()
		{
			if (HasKitchenObject())
			{
				switch (_state)
				{
					case State.Idle:
						break;
					case State.Frying:

						_fryingTimer += Time.deltaTime;

						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
							ProgressNormalized = _fryingTimer / _fryingRecipeData.FryingTimerMax,
						});

						if (_fryingTimer > _fryingRecipeData.FryingTimerMax)
						{
							GetKitchenObject().DestroySelf();

							InstantiationKitchenObject(_fryingRecipeData.Output, this);

							_state = State.Fried;

							_burningRecipeData = GetBurningRecipeDataWithInput(GetKitchenObject().GetKitchenProductsData());

							OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
								state = _state,
							});
						}

						break;
					case State.Fried:

						_burningTimer += Time.deltaTime;

						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
							ProgressNormalized = _burningTimer / _burningRecipeData.BurningTimerMax,
						});

						if (_burningTimer > _burningRecipeData.BurningTimerMax)
						{
							GetKitchenObject().DestroySelf();

							InstantiationKitchenObject(_burningRecipeData.Output, this);

							_state = State.Burned;

							OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
								state = _state,
							});

							OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
								ProgressNormalized = 0,
							});
						}
						break;
					case State.Burned:

						_burningTimer = 0;
						break;
				}
			}
		}

		public override void Interact(HeroInteractions heroInteraction)
		{
			if (!HasKitchenObject())
			{
				if (heroInteraction.HasKitchenObject())
				{
					if (HasRecipeWithInput(heroInteraction.GetKitchenObject().GetKitchenProductsData()))
					{
						heroInteraction.GetKitchenObject().SetKitchenObjectParent(this);

						_fryingRecipeData = GetFryingRecipeDataWithInput(GetKitchenObject().GetKitchenProductsData());

						_state = State.Frying;
						
						_fryingTimer = 0f;
						
						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
							state = _state,
						});

						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
							ProgressNormalized = _fryingTimer / _fryingRecipeData.FryingTimerMax,
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
							
							_state = State.Idle;

							OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
								state = _state,
							});
					
							OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
								ProgressNormalized = 0,
							});
						}
					}
				}
				else
				{
					GetKitchenObject().SetKitchenObjectParent(heroInteraction);

					_state = State.Idle;

					OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
						state = _state,
					});
					
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
						ProgressNormalized = 0,
					});
				}
			}
		}

		private bool HasRecipeWithInput(KitchenProductsData inputKitchenProductsData)
		{
			FryingRecipeData fryingRecipeData = GetFryingRecipeDataWithInput(inputKitchenProductsData);
			return fryingRecipeData != null;
		}

		private KitchenProductsData GetOutputForInput(KitchenProductsData inputKitchenProductsData)
		{
			FryingRecipeData fryingRecipeData = GetFryingRecipeDataWithInput(inputKitchenProductsData);

			if (fryingRecipeData != null)
				return fryingRecipeData.Output;
			else
				return null;
		}

		private FryingRecipeData GetFryingRecipeDataWithInput(KitchenProductsData inputKitchenProductsData)
		{
			foreach (FryingRecipeData fryingRecipeData in _fryingRecipeDatas)
				if (fryingRecipeData.Input == inputKitchenProductsData)
					return fryingRecipeData;

			return null;
		}

		private BurningRecipeData GetBurningRecipeDataWithInput(KitchenProductsData inputKitchenProductsData)
		{
			foreach (BurningRecipeData burningRecipeData in _burningRecipeDatas)
				if (burningRecipeData.Input == inputKitchenProductsData)
					return burningRecipeData;

			return null;
		}

	}

}