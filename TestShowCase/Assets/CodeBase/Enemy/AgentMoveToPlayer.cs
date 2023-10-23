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

		private const float MinimalDistance = 1.1f;

		public bool CanMove { get; set; }

		//private IGameFactory _gameFactory;
		public Transform _heroTransform;
		private ITimerService _timerService;
		private float _cachedSpeed;


		public void Construct(Transform heroTransform, ITimerService timerService)
		{
			_heroTransform = heroTransform;
			_timerService = timerService;
		}

		private void Start()
		{
			_cachedSpeed = Agent.speed;
			CanMove = true;
		}

		private void Update()
		{
			if (_heroTransform && IsHeroNotReached())
				Agent.destination = _heroTransform.position;

			if (_timerService.IsTimerActive && CanMove)
				StartMoving();
			else
				StopMoving();
		}

		public void StartMoving() =>
			Agent.speed = _cachedSpeed;

		public void StopMoving() =>
			Agent.speed = 0;

		private bool IsHeroNotReached() =>
			Agent.transform.position.SqrMagnitudeTo(_heroTransform.position) >= MinimalDistance;
	}
}