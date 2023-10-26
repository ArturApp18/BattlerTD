using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.Timers;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
	public class AgentMoveToPlayer : Follow
	{
		public NavMeshAgent Agent;
		public EnemyAttack _enemyAttack;

		private const float MinimalDistance = 1.1f;

		public bool CanMove { get; set; }

		//private IGameFactory _gameFactory;
		public Transform _targetTransform;
		public Transform _cachedPumpkinTransform;
		private float _cachedSpeed;

		public Transform TargetTransform
		{
			get
			{
				return _targetTransform;
			}
			set
			{
				_targetTransform = value;
				_enemyAttack.Construct(_targetTransform);
			}
		}

		public void Construct(Transform pumpkinTransform) =>
			TargetTransform = pumpkinTransform;

		private void Start()
		{
			_cachedSpeed = Agent.speed;
			_cachedPumpkinTransform = _targetTransform;
			CanMove = true;
		}

		private void Update()
		{
			if (TargetTransform && IsHeroNotReached())
				Agent.destination = TargetTransform.position;

			if (CanMove)
				StartMoving();
			else
				StopMoving();
		}

		public void ChangeToNewTarget(Transform newTarget)
		{
			Agent.stoppingDistance = 1.5f;
			TargetTransform = newTarget;
		}

		public void ChangeToOldTarget()
		{
			Agent.stoppingDistance = 2.2f;
			TargetTransform = _cachedPumpkinTransform;
		}

		public void StartMoving() =>
			Agent.speed = _cachedSpeed;

		public void StopMoving() =>
			Agent.speed = 0;

		private bool IsHeroNotReached() =>
			Agent.transform.position.SqrMagnitudeTo(TargetTransform.position) >= MinimalDistance;
	}
}