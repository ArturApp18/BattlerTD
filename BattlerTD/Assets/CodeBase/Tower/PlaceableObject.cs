using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Tower
{
	public class PlaceableObject : MonoBehaviour
	{
		[SerializeField] private GameObject _placeableVisual;
		[SerializeField] private GameObject _gameVisual;
		[SerializeField] private MeshRenderer _towerMesh;
		[SerializeField] private MeshRenderer _turretMesh;
		
		public bool Placed { get; private set; }
		public Vector3Int Size { get; private set; }
		private Vector3[] Vertices;
		private BoxCollider _collider;
		

		private void Start()
		{
			GetColliderVertexPositionsLocal();
			CalculateSizeInCells();
		}

		private void Update()
		{
			if (Placed)
				return;

			if (BuildingHandler.Current.CanBePlaced(this))
			{
				for (int i = 0; i < _towerMesh.materials.Length; i++)
				{
					_towerMesh.materials[i].color = Color.green;
				}

				for (int i = 0; i < _turretMesh.materials.Length; i++)
				{
					_turretMesh.materials[i].color = Color.green;
				}
			}
			else
			{
				for (int i = 0; i < _towerMesh.materials.Length; i++)
				{
					_towerMesh.materials[i].color = Color.red;
				}

				for (int i = 0; i < _turretMesh.materials.Length; i++)
				{
					_turretMesh.materials[i].color = Color.red;
				}
			}
		}

		public Vector3 GetStartPosition() =>
			transform.TransformPoint(Vertices[0]);

		private void GetColliderVertexPositionsLocal()
		{
			_collider = gameObject.GetComponent<BoxCollider>();
			Vertices = new Vector3[4];
			Vertices[0] = _collider.center + new Vector3(-_collider.size.x, -_collider.size.y, -_collider.size.z) * 0.5f;
			Vertices[1] = _collider.center + new Vector3(_collider.size.x, -_collider.size.y, -_collider.size.z) * 0.5f;
			Vertices[2] = _collider.center + new Vector3(_collider.size.x, -_collider.size.y, _collider.size.z) * 0.5f;
			Vertices[3] = _collider.center + new Vector3(-_collider.size.x, -_collider.size.y, _collider.size.z) * 0.5f;
		}

		private void CalculateSizeInCells()
		{
			Vector3Int[] vertices = new Vector3Int[Vertices.Length];

			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 worldPosition = transform.TransformPoint(Vertices[i]);
				vertices[i] = BuildingHandler.Current.GridLayout.WorldToCell(worldPosition);
			}

			Size = new Vector3Int(Math.Abs(( vertices[0] - vertices[1] ).x), Math.Abs(( vertices[0] - vertices[3] ).y), 1);
		}

		public void Place()
		{
			_placeableVisual.SetActive(false);
			_gameVisual.SetActive(true);
			ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
			Destroy(drag);
			_collider.enabled = true;
			Placed = true;
		}
	}

}