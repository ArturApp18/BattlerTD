using System;
using CodeBase.Infrastructure.Services.Skills;
using CodeBase.Logic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Hero.Skills
{
	public class Atom : Skill
	{
		[SerializeField] private Transform _target;
		[SerializeField] private float _rotationSpeed;
		[SerializeField] private float _radius;
		[SerializeField] private float _damage;
		[SerializeField] private GameObject _impactVFX;

		
		private void Update()
		{
			Vector3 newPosition = transform.parent.position + new Vector3(Mathf.Cos(Time.time * _rotationSpeed) * _radius, 0f, Mathf.Sin(Time.time * _rotationSpeed) * _radius);
			
			transform.position = newPosition;
		}

		private void OnTriggerEnter(Collider other)
		{
			Debug.Log(other.name);
			other.transform.GetComponentInChildren<IHealth>().TakeDamage(_damage * _progressService.Progress.HeroStats.Damage);
			Instantiate(_impactVFX, other.transform.position, Quaternion.identity);
		}
	}
}