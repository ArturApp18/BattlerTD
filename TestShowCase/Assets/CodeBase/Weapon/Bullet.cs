using System;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Weapon
{
	public class Bullet : MonoBehaviour
	{
		private IPersistentProgressService _progressService;
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private float _damage;
		[SerializeField] private GameObject _explosionVFX;

		public float _moveSpeed;

		public void Construct(IPersistentProgressService progressService)
		{
			_progressService = progressService;
		}

		private void Start()
		{
			Destroy(gameObject, 7);
		}

		private void Update()
		{
			_rigidbody.velocity = transform.forward * (_moveSpeed * Time.deltaTime);
		}

		private void OnTriggerEnter(Collider other)
		{
			Debug.Log(other.name);
			other.transform.GetComponentInChildren<IHealth>().TakeDamage(_damage * _progressService.Progress.HeroStats.Damage);
			Instantiate(_explosionVFX, other.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}

	}

}