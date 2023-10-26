using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Tower
{
	public class Arrow : MonoBehaviour
	{
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private float _damage;
		[SerializeField] private GameObject _explosionVFX;
		
		public float MoveSpeed;

		private void Start()
		{
			Destroy(gameObject, 5);
		}

		private void Update()
		{
			_rigidbody.velocity = -transform.forward * (MoveSpeed * Time.deltaTime);
		}

		private void OnTriggerEnter(Collider other)
		{
			Debug.Log(other.name);
			other.transform.GetComponentInParent<IHealth>().TakeDamage(_damage);
			//Instantiate(_explosionVFX, other.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}