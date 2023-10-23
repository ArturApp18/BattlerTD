using System;
using System.Linq;
using UnityEngine;

namespace CodeBase.Hero
{
	public class CheckClosestEnemy : MonoBehaviour
	{
		private Collider[] _hits = new Collider[1];
		private int _layerMask;
		private Transform _closestEnemy;
		public Vector3 Cleavage;
		public Transform ClosestEnemy => _closestEnemy;


		private void Awake() =>
			_layerMask = 1 << LayerMask.NameToLayer("Enemy");

		private void Update()
		{
			if (Hit(out Collider hit))
			{
				_closestEnemy = hit.transform;
			}
			else
			{
				_closestEnemy = null;
			}
		}
		

		private bool Hit(out Collider hit)
		{
			int hitAmount = Physics.OverlapBoxNonAlloc(transform.position, Cleavage, _hits, Quaternion.identity, _layerMask);

			hit = _hits.FirstOrDefault();

			return hitAmount > 0;
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(transform.position, Cleavage * 2);
		}
	}
}