using System;
using Counters.CountersLogic;
using Logic;
using TMPro;
using UnityEngine;

namespace UI
{
	public class GameOverUI : MonoBehaviour
	{
		[SerializeField] private OrderReceiver _orderReceiver;
		[SerializeField] private TextMeshProUGUI _gameOverText;
		
		private void Start()
		{
			GameLoop.Instance.OnStateChanged += OnStateChanged;
			Hide();
		}



		private void OnStateChanged(object sender, EventArgs e)
		{
			if (GameLoop.Instance.IsGameOver())
			{
				Show();
				_gameOverText.text = _orderReceiver.GetSuccessfulAmount().ToString();
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