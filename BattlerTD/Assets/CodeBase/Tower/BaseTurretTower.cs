using System.Collections;
using CodeBase.Weapon;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Tower
{

	public class BaseTurretTower : MonoBehaviour
	{
		[SerializeField] private CheckClosestTarget _checkClosestTarget;
		[SerializeField] private Transform _shootPosition;
		[SerializeField] private float _attackCooldown = 3.0f;
		[SerializeField] private GameObject _bullet;
		[SerializeField] private GameObject _shootFx;
		[SerializeField] private float _delayBetweenShoot;

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

			Instantiate(_bullet, _shootPosition.position, Quaternion.identity).TryGetComponent(out Bullet bullet);
			//Instantiate(_shootFx, _shootPosition.position, Quaternion.identity);
			bullet.transform.LookAt(_checkClosestTarget.Target);
			yield return new WaitForSeconds(_delayBetweenShoot);

			_isAttacking = false;
		}

		private void UpdateCooldown()
		{
			if (!CooldownIsUp())
				_attackCooldown -= Time.deltaTime;
		}
	}

}