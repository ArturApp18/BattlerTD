using System;
using Logic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class GamePauseUI : MonoBehaviour
	{
		private const string MainMenu = "MainMenu";
		[SerializeField] private Button _resumeButton;
		[SerializeField] private Button _mainMenuButton;

		private void Awake()
		{
			_resumeButton.onClick.AddListener(() =>
			{
				GameLoop.Instance.TogglePauseGame();
			});
			_mainMenuButton.onClick.AddListener( () =>
			{
				Loader.Load(MainMenu);
			});
		}

		private void Start()
		{
			GameLoop.Instance.OnGamePaused += OnGamePaused;
			GameLoop.Instance.OnGameUnpaused += OnGameUnpaused;
			
			Hide();
		}

		private void OnGameUnpaused(object sender, EventArgs e) =>
			Hide();

		private void OnGamePaused(object sender, EventArgs e) =>
			Show();

		private void Show() =>
			gameObject.SetActive(true);

		private void Hide() =>
			gameObject.SetActive(false);
	}
}