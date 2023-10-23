using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
	[CreateAssetMenu(menuName = "RecipeData", fileName = "RecipeData", order = 0)]
	public class RecipeData : ScriptableObject
	{
		public List<KitchenProductsData> KitchenProductsData;
		public string Name;
	}
}