using System;
using System.Collections.Generic;
using UnityEngine;

namespace Counters
{
	public class PlatesCounterVisual : MonoBehaviour
	{
		[SerializeField] private Transform _counterTopPoint;
		[SerializeField] private PlatesCounter _platesCounter;
		[SerializeField] private Transform _plateVisualPrefab;


		private List<GameObject> _platesVisualGameObjects;

		private void Awake()
		{
			_platesVisualGameObjects = new List<GameObject>();
		}

		private void Start()
		{
			_platesCounter.OnPlateSpawned += PlatesCounterOnPlateSpawned;
			_platesCounter.OnPlateRemoved += PlatesCounterOnPlateRemoved;
		}

		private void PlatesCounterOnPlateRemoved(object sender, EventArgs e)
		{
			GameObject plateGameObject = _platesVisualGameObjects[_platesVisualGameObjects.Count - 1];
			_platesVisualGameObjects.Remove(plateGameObject);
			Destroy(plateGameObject);
		}

		private void PlatesCounterOnPlateSpawned(object sender, EventArgs e)
		{
			Transform plateVisualTransform = Instantiate(_plateVisualPrefab, _counterTopPoint);
			float plateOffsetY = 0.1f;
			plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * _platesVisualGameObjects.Count, 0);
			
			_platesVisualGameObjects.Add(plateVisualTransform.gameObject);
		}
	}

}