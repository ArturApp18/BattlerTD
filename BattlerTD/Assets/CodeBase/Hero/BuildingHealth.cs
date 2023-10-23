using System;
using System.Collections;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
	public class BuildingHealth : MonoBehaviour, IHealth, ISavedProgressReader
	{
		public MainBuildingAnimator Animator;
		private State _state;

		public event Action HealthChanged;
		public Action Restart;
		[SerializeField] private float _delayBeforeRestart = 2;
		public float Current
		{
			get => _state.CurrentHP;
			set
			{
				if (value != _state.CurrentHP)
				{
					_state.CurrentHP = value;
          
					HealthChanged?.Invoke();
				}
			}
		}

		public float Max
		{
			get => _state.MaxHP;
			set => _state.MaxHP = value;
		}
		public void TakeDamage(float damage)
		{
			if (Current <= 0)
			{
				Die();
				return;
			}

			transform.localScale *= 0.99f;
			Current -= damage;
			Animator.PlayHit();
		}

		private void Die() =>
			StartCoroutine(RestartGame());

		public void LevelUp()
		{
      
		}

		public void LoadProgress(PlayerProgress progress)
		{
			_state = progress.HeroState;
			HealthChanged?.Invoke();
		}
		
		private IEnumerator RestartGame()
		{
			yield return new WaitForSeconds(_delayBeforeRestart);
			Restart?.Invoke();
		}
	}
}