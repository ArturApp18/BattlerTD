using System;
using System.Globalization;
using Logic;
using TMPro;
using UnityEngine;

namespace UI
{
	public class GameStartCountdownUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _countdownText;
		

		private void Start()
		{
			GameLoop.Instance.OnStateChanged += OnStateChanged;
			Hide();
		}

		private void Update()
		{
			_countdownText.text = Mathf.Ceil(GameLoop.Instance.GetCountdownToStartTimer()).ToString();
		}
		private void OnStateChanged(object sender, EventArgs e)
		{
			if (GameLoop.Instance.IsCountdownToStartActive())
			{
				Show();
				
			}
			else
			{
				Hide();
			}
		}

		private void Show() =>
			gameObject.SetActive(true);

		private void Hide() =>
			gameObject.SetActive(false);
	}

}