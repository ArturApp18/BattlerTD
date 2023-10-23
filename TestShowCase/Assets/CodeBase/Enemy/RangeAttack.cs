using System.Collections;
using CodeBase.Hero;
using CodeBase.Weapon;
using UnityEngine;

namespace CodeBase.Enemy
{
	[RequireComponent(typeof(EnemyAnimator))]
	public class RangeAttack : MonoBehaviour
	{
		public EnemyAnimator Animator;
		public AgentMoveToPlayer MoveToPlayer;
		public CheckClosestEnemy ClosestEnemy;

		public float AttackCooldown = 3.0f;
		[SerializeField] private GameObject _bullet;
		[SerializeField] private GameObject _shootVFX;
		private bool _isShooting;

		private void Update()
		{
			if (ClosestEnemy.ClosestEnemy != null && _isShooting)
			{
				MoveToPlayer.StopMoving();
				StartCoroutine(Shoot());
			}
			else if (ClosestEnemy.ClosestEnemy == null)
			{
				MoveToPlayer.StartMoving();
			}
		}

		private IEnumerator Shoot()
		{
			_isShooting = true;
			
			BulletEnemy bulletEnemy = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<BulletEnemy>();
			bulletEnemy.transform.LookAt(ClosestEnemy.ClosestEnemy);
			Instantiate(_shootVFX, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(AttackCooldown);
		
			_isShooting = false;
		}

	}
}