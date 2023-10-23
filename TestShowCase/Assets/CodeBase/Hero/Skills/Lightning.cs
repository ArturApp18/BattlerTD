using System.Collections;
using CodeBase.Infrastructure.Services.Skills;
using UnityEngine;

namespace CodeBase.Hero.Skills
{

	public class Lightning : Skill
	{
		[SerializeField] private CheckClosestEnemy _closestEnemyLocator;

		public GameObject _bullet;
		public float _shooingRate;
		private bool _isShooting;

		private void Update()
		{
			if (_isShooting)
				return;

			//transform.LookAt(_closestEnemyLocator.ClosestEnemy.position);
			StartCoroutine(ShootingCoroutine());
		}

		private LightningProjectile Shoot()
		{
			LightningProjectile bullet = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<LightningProjectile>();
			return bullet;
		}

		private IEnumerator ShootingCoroutine()
		{
			_isShooting = true;

			LightningProjectile firstBullet = Shoot();
			firstBullet.Construct(_progressService);
			firstBullet.transform.LookAt(_closestEnemyLocator.ClosestEnemy);

			yield return new WaitForSeconds(_shooingRate);
			_isShooting = false;
		}
	}
}