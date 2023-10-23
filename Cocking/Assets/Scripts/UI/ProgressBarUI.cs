using Counters;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class ProgressBarUI : MonoBehaviour
	{
		[SerializeField] private GameObject _hasProgressGameObject;
		[SerializeField] private Image _barImage;
		
		private IHasProgress _hasProgress;


		private void Start()
		{
			_hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();
			if (_hasProgress == null)
			{
				Debug.LogError("Game Object" + _hasProgressGameObject + "Does not have that I has Progeres");
			}
			_hasProgress.OnProgressChanged += HasProgressChanged;

			_barImage.fillAmount = 0f;
			
			Hide();
		}

		private void HasProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
		{
			_barImage.fillAmount = e.ProgressNormalized;

			if (e.ProgressNormalized == 0f || e.ProgressNormalized == 1f)
			{
				Hide();
			}
			else
			{
				Show();
			}
		}

		private void Show()
		{
			gameObject.SetActive(true);
		}

		private void Hide()
		{
			gameObject.SetActive(false);
		}

		
	}

}