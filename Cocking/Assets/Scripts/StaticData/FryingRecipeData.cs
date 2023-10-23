using UnityEngine;

namespace StaticData
{
	[CreateAssetMenu(fileName = "FryingRecipe", menuName = "FryingRecipe")]
	public class FryingRecipeData : ScriptableObject
	{
		public KitchenProductsData Input;
		public KitchenProductsData Output;
		public float FryingTimerMax;
	}

}