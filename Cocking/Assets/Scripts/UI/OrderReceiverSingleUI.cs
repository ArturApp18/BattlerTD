using StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class OrderReceiverSingleUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI recipeNameText;
		[SerializeField] private Transform _iconContainer;
		[SerializeField] private Transform _iconTemplate;

		private void Awake()
		{
			_iconTemplate.gameObject.SetActive(false);
		}

		public void SetRecipeData(RecipeData recipeData)
		{
			recipeNameText.text = recipeData.Name;

			foreach (Transform child in _iconContainer)
			{
				if (child == _iconTemplate)
					continue;
				
				Destroy(child.gameObject);
			}

			foreach (KitchenProductsData kitchenProductsData in recipeData.KitchenProductsData)
			{
				Transform iconTransform = Instantiate(_iconTemplate, _iconContainer);
				iconTransform.gameObject.SetActive(true);
				iconTransform.GetComponent<Image>().sprite = kitchenProductsData.Sprite;
			}
		}
	}

}