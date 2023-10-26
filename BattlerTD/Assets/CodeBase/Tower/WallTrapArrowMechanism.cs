using System.Collections;
using UnityEngine;

namespace CodeBase.Tower
{
	public class WallTrapArrowMechanism : MonoBehaviour
	{
		[SerializeField] private ArrowHandler[] _arrows;
		[SerializeField] private int _attackAmount;
		[SerializeField] private float _delayBetweenShoot;
		[SerializeField] private float _delayBetweenTrigger;

		private Coroutine _shootCoroutine;

		private void OnTriggerEnter(Collider other)
		{
			if (_shootCoroutine == null)
				_shootCoroutine = StartCoroutine(Shoot(other.transform));
		}

		private IEnumerator Shoot(Transform enemyTransform)
		{
			for (int i = 0; i < _attackAmount; i++)
			{
				_arrows[i].PushArrow(enemyTransform);
				yield return new WaitForSeconds(_delayBetweenShoot);
			}

			yield return new WaitForSeconds(_delayBetweenTrigger);
			_shootCoroutine = null;
		}
	}
}