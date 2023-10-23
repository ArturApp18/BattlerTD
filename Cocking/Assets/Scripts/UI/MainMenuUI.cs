using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class MainMenuUI : MonoBehaviour
	{
		private const string Main = "Main";
		
		[SerializeField] private Button _playButton;
		[SerializeField] private Button _quitButton;

		private void Awake()
		{
			_playButton.onClick.AddListener(() =>
			{
				Loader.Load(Main);
			});
			_quitButton.onClick.AddListener(() =>
			{
				Application.Quit();
			});

			Time.timeScale = 1f;
		}
	}

}