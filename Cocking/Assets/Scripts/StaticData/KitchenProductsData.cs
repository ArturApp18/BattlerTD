using UnityEngine;

namespace StaticData
{
	[CreateAssetMenu(fileName = "Products", menuName = "Kitchen")]
	public class KitchenProductsData : ScriptableObject
	{
		public Transform Prefab;
		public Sprite Sprite;
		public string _name;
	}
}