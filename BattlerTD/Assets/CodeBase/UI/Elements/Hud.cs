using CodeBase.Tower;
using DG.Tweening;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class Hud : MonoBehaviour
	{
		[SerializeField] private RectTransform _attackButtonInGamePosition;
		[SerializeField] private RectTransform _attackButtonStartPosition;
		[SerializeField] private RectTransform _towerPanelStartPosition;
		[SerializeField] private RectTransform _towerPanelInGamePosition;
		[SerializeField] private RectTransform _attackButton;
		[SerializeField] private RectTransform _towerPanel;
		[SerializeField] private RectTransform _hudTransform;
		[SerializeField] private float _appearDuration;

		private Vector3 _startPosition;
		private IBuildingService _buildingService;
		public RectTransform HUDTransform
		{
			get
			{
				return _hudTransform;
			}
			set
			{
				_hudTransform = value;
			}
		}

		public void Construct(IBuildingService buildingService) =>
			_buildingService = buildingService;

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
			_buildingService.IsActive = false;
			_attackButton.DOMoveX(_attackButtonInGamePosition.position.x, _appearDuration);
		}

		public void DisappearAttackButton()
		{
			_attackButton.DOMove(_attackButtonStartPosition.position, _appearDuration);
		}

		public void DisappearTowerPanel() =>
			_towerPanel.DOMove(_towerPanelStartPosition.position, _appearDuration);

		public void AppearTowerPanel()
		{
			_towerPanel.DOMove(_towerPanelInGamePosition.position, _appearDuration).OnComplete(() =>
			{
				_buildingService.IsActive = true;
			});
		}
	}
}