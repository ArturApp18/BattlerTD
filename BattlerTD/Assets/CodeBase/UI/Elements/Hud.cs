using CodeBase.Tower;
using DG.Tweening;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class Hud : MonoBehaviour
	{
		[SerializeField] private Transform _attackButtonInGamePosition;
		[SerializeField] private Transform _attackButtonStartPosition;
		[SerializeField] private Transform _towerPanelStartPosition;
		[SerializeField] private Transform _towerPanelInGamePosition;
		[SerializeField] private Transform _attackButton;
		[SerializeField] private Transform _towerPanel;
		[SerializeField] private float _appearDuration;

		private Vector3 _startPosition;

		private void Start()
		{
			ButtonInputUI[] buttons = _towerPanel.GetComponentsInChildren<ButtonInputUI>();
			foreach (ButtonInputUI button in buttons)
			{
				button.enabled = false;
			}
		}

		public void AppearAttackButton()
		{
			_attackButton.DOMove(_attackButtonInGamePosition.position, _appearDuration).OnComplete(() =>
			{
				BuildingHandler.Current.IsActive = false;
			});
		}

		public void DisappearAttackButton()
		{
			ButtonInputUI[] buttons = _towerPanel.GetComponentsInChildren<ButtonInputUI>();
			foreach (ButtonInputUI button in buttons)
			{
				button.enabled = false;
			}

			_attackButton.DOMove(_attackButtonStartPosition.position, _appearDuration);
		}

		public void DisappearTowerPanel() =>
			_towerPanel.DOMove(_towerPanelStartPosition.position, _appearDuration);

		public void AppearTowerPanel()
		{
			ButtonInputUI[] buttons = _towerPanel.GetComponentsInChildren<ButtonInputUI>();
			foreach (ButtonInputUI button in buttons)
			{
				button.enabled = true;
			}

			_towerPanel.DOMove(_towerPanelInGamePosition.position, _appearDuration).OnComplete(() =>
			{
				BuildingHandler.Current.IsActive = true;
			});
		}
	}
}