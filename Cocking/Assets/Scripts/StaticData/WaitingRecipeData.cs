using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
	[CreateAssetMenu(fileName = "WaitingRecipeData", menuName = "WaitingRecipeData")]
	public class WaitingRecipeData : ScriptableObject
	{
		public List<RecipeData> waitingRecipeData;
	}
}