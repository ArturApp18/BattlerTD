using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.UI.Windows;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class TowerPanel : MonoBehaviour
	{
		[SerializeField] private Transform _inGamePosition;
		[SerializeField] private Button _nextStageButton;
		[SerializeField] private float _appearDuration;

		private Vector3 _startPosition;
		private IGameStateMachine _stateMachine;

		private void Awake()
		{
			_stateMachine = AllServices.Container.Single<IGameStateMachine>();
		}

		private void Start()
		{
			_nextStageButton.onClick.AddListener(NextWave);
			_startPosition = transform.position;
		}

		private void NextWave()
		{
			_stateMachine.Enter<GameLoopAttackState>();
		}

		public void Appear() =>
			transform.DOMove(_inGamePosition.position, _appearDuration);

		public void Disappear() =>
			transform.DOMove(_startPosition, _appearDuration);
	}
}