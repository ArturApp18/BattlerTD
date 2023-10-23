using StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class PlateIconsSingleUI : MonoBehaviour
	{
		[SerializeField] private Image _image;

		public void SetKitchenObjectData(KitchenProductsData kitchenProductsData)
		{
			_image.sprite = kitchenProductsData.Sprite;
		}
	}
}