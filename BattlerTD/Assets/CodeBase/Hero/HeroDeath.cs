using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Hero
{
	public class HeroDeath : MonoBehaviour
	{
		public HeroHealth Health;

		public HeroMove Move;
		public HeroAnimator Animator;

		public GameObject DeathFx;
		private bool _isDead;
		public Action Happend;
		public Action Restart;
		[SerializeField]
		private float _delayBeforeRestart = 2.5f;

		private void Start()
		{
			Health.HealthChanged += HealthChanged;
		}

		private void OnDestroy()
		{
			Health.HealthChanged -= HealthChanged;
		}

		private void HealthChanged()
		{
			if (!_isDead && Health.Current <= 0)
				Die();
		}

		private void Die()
		{
			_isDead = true;
			Move.enabled = false;
			Animator.PlayDeath();
			Happend?.Invoke();
			//Instantiate(DeathFx, transform.position, Quaternion.identity);
			StartCoroutine(HeroSlayed());
		}

		private IEnumerator HeroSlayed()
		{
			yield return new WaitForSeconds(_delayBeforeRestart);
			Restart?.Invoke();
		}
	}
}