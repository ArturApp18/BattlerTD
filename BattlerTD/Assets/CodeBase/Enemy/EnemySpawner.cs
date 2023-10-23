using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private GameObject _enemyPrefab;
		[SerializeField] private float _spawnRate;

		private bool _isPlaying;

		private void Start()
		{
			_isPlaying = true;
			StartCoroutine(SpawnEnemy());
		}

		private IEnumerator SpawnEnemy()
		{
			while (_isPlaying)
			{
				Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

				yield return new WaitForSeconds(_spawnRate);
			}

			yield return null;
		}
	}
}