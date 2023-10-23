using System;
using System.Collections;
using CodeBase.Weapon;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Tower
{
	public class BaseTurretTower : MonoBehaviour
	{
		private static readonly int IsAttack = Animator.StringToHash("IsAttack");
		
		[SerializeField] private CheckClosestTarget _checkClosestTarget;
		[SerializeField] private Transform _shootPosition;
		[SerializeField] private float _attackCooldown = 3.0f;
		[SerializeField] private GameObject _bullet;
		[SerializeField] private GameObject _shootFx;
		[SerializeField] private float _delayBetweenShoot;
		[SerializeField] private Animator _animator;
		private bool _isActive;
		private bool _isAttacking;

		private void Update()
		{
			UpdateCooldown();

			if (CanAttack())
				StartCoroutine(StartAttack());
		}
		
		private bool CooldownIsUp() =>
			_attackCooldown <= 0f;

		private bool CanAttack() =>
			_checkClosestTarget.Target && !_isAttacking && CooldownIsUp();

		private IEnumerator StartAttack()
		{
			_isAttacking = true;
			_animator.SetBool(IsAttack, _isAttacking);
			Bullet bullet = Instantiate(_bullet, _shootPosition.position, Quaternion.identity).GetComponent<Bullet>();
			Instantiate(_shootFx, _shootPosition.position, Quaternion.identity);
			bullet.transform.LookAt(_checkClosestTarget.Target);
			yield return new WaitForSeconds(_delayBetweenShoot);
			Debug.Log("alojjjjji");
			_isAttacking = false;
			_animator.SetBool(IsAttack, _isAttacking);
		}

		private void UpdateCooldown()
		{
			if (!CooldownIsUp())
				_attackCooldown -= Time.deltaTime;
		}
	}

}