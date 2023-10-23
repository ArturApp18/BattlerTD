using System.Linq;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
	[RequireComponent(typeof(EnemyAnimator))]
	public class EnemyAttack : MonoBehaviour
	{
		public EnemyAnimator Animator;
		public AgentMoveToPlayer MoveToPlayer;

		public float AttackCooldown = 3.0f;
		public float Cleavage = 0.5f;
		public float EffectiveDistance = 0.5f;
		public float Damage = 10;

		private Transform _heroTransform;
		private Collider[] _hits = new Collider[1];
		private int _layerMask;
		private float _attackCooldown;
		private bool _isAttacking;
		private bool _attackIsActive;
		
		[SerializeField] private GameObject ImpactFxPrefab;

		public void Construct(Transform heroTransform) =>
			_heroTransform = heroTransform;

		private void Awake() =>
			_layerMask = 1 << LayerMask.NameToLayer("MainBuilding") | 1 << LayerMask.NameToLayer("Player");

		private void Update()
		{
			UpdateCooldown();

			if (CanAttack())
				StartAttack();
		}

		private void OnAttack()
		{
			if (Hit(out Collider hit))
			{
				Debug.Log(hit.name);
				//PhysicsDebug.DrawDebugCross(StartPoint(), Cleavage, 1.0f);
				hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
				PlayTakeDamageFx(hit.transform.position);
			}
		}

		private void OnAttackEnded()
		{
			MoveToPlayer.enabled = true;
			_attackCooldown = AttackCooldown;
			_isAttacking = false;
		}

		public void DisableAttack() =>
			_attackIsActive = false;

		public void EnableAttack() =>
			_attackIsActive = true;

		private bool CooldownIsUp() =>
			_attackCooldown <= 0f;

		private void UpdateCooldown()
		{
			if (!CooldownIsUp())
				_attackCooldown -= Time.deltaTime;
		}

		private bool Hit(out Collider hit)
		{
			int hitAmount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

			hit = _hits.FirstOrDefault();

			return hitAmount > 0;
		}

		private void OnDrawGizmos() =>
			Gizmos.DrawWireSphere(StartPoint(), Cleavage);

		private Vector3 StartPoint() =>
			new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
			transform.forward * EffectiveDistance;

		private bool CanAttack() =>
			_attackIsActive && !_isAttacking && CooldownIsUp();

		private void PlayTakeDamageFx(Vector3 position) =>
			Instantiate(ImpactFxPrefab, position, Quaternion.identity);
		
		private void StartAttack()
		{
			MoveToPlayer.enabled = false;
			transform.LookAt(_heroTransform);
			Animator.PlayAttack();
			_isAttacking = true;
		}
	}
}