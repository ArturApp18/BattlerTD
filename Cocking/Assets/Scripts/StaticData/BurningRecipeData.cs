using UnityEngine;

namespace StaticData
{
	[CreateAssetMenu(menuName = "BurningRecipeData", fileName = "BurningRecipeData", order = 0)]
	public class BurningRecipeData : ScriptableObject
	{
		public KitchenProductsData Input;
		public KitchenProductsData Output;
		public float BurningTimerMax;
	}

} 