using UnityEngine;

namespace StaticData
{
	[CreateAssetMenu(fileName = "CuttingRecipe", menuName = "ProductRecipe")]
	public class CuttingRecipeData : ScriptableObject
	{
		public KitchenProductsData Input;
		public KitchenProductsData Output;
		public int CuttingProgressMax;
	}

}