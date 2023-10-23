using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private GameObject _enemyPrefab;
		[SerializeField] private Transform _heroTransform;
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
				GameObject enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
		
				enemy.GetComponent<RotateToHero>().Construct(_heroTransform);
				yield return new WaitForSeconds(_spawnRate);
			}

			yield return null;
		}
	}
}