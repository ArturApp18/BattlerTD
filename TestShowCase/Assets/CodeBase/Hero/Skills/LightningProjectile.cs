using System.Collections;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero.Skills
{
	public class LightningProjectile : MonoBehaviour
	{
		private IPersistentProgressService _progressService;
		
		[SerializeField] private float _damage;
		[SerializeField] private GameObject _impactVFX;
		[SerializeField] private CheckClosestEnemy _closestEnemy;
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private float _moveSpeed;
		[SerializeField] private int _maxChainCount = 2;
		[SerializeField] private float _lightningAttack;

		private Transform _target;
		private bool _isMoving;
		private bool _isAttacking;

		public void Construct(IPersistentProgressService progressService)
		{
			_progressService = progressService;
		}

		private void Awake()
		{
			_isMoving = true;
		}

		private void Update()
		{
			if (_isMoving)
			{
				_rigidbody.velocity = transform.forward * ( _moveSpeed * Time.deltaTime );
			}
			else
			{
				_rigidbody.velocity = Vector3.zero;
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			StartCoroutine(LightningAttack(other.transform));
		}

		private IEnumerator LightningAttack(Transform target)
		{
			if (_lightningAttack >= _maxChainCount)
				Destroy(gameObject); 
			
			DealDamage(target);
			PlayImpactVFX(target);
			_isMoving = false;
			transform.LookAt(_closestEnemy.ClosestEnemy);
			yield return new WaitForSeconds(0.1f);
			_isMoving = true;
			_lightningAttack++;
			
		}

		private void PlayImpactVFX(Transform target) =>
			Instantiate(_impactVFX, target.transform.position, Quaternion.identity);

		private void DealDamage(Transform target)
		{
			if (target != null)
				target.transform.GetComponentInChildren<IHealth>().TakeDamage(_damage * _progressService.Progress.HeroStats.Damage);
		}

	}
}