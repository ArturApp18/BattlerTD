using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Inputs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CodeBase.Tower
{
	public class BuildingHandler : MonoBehaviour
	{
		public static BuildingHandler Current;
		public GridLayout GridLayout;
		private Grid _grid;

		[SerializeField] private MeshRenderer[] _ableToPlaceVisuals;
		[SerializeField] private Tilemap _tilemap;
		[SerializeField] private TileBase _tileBase;

		public GameObject TurretTower;
		public GameObject RadiusTower;

		private PlaceableObject _objectToPlace;
		private IInputService _input;
		private static Ray _ray;
		private bool _ableToPlace;
		private bool _isActive;
		public bool IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
			}
		}

		private void Awake()
		{
			Current = this;
			_input = AllServices.Container.Single<IInputService>();
			_input.FirstTowerTap = false;
			_input.SecondTowerTap = false;
			_grid = GridLayout.gameObject.GetComponent<Grid>();
		}

		private void Update()
		{
			if (IsActive)
			{
				if (_input.IsTurretTowerButtonDown())
				{
					if (_objectToPlace)
						return;
					InitializeWithObject(TurretTower);
				}
				else if (_input.IsRadiusTowerButtonDown())
				{
					if (_objectToPlace)
						return;
					InitializeWithObject(RadiusTower);
				}

				if (!_objectToPlace)
					return;
			
				if (_input.IsTurretTowerButtonUp() || _input.IsRadiusTowerButtonUp())
				{
					Debug.Log(_input.SecondTowerTap);
					if (CanBePlaced(_objectToPlace))
					{
						_objectToPlace.Place();
						Vector3Int start = GridLayout.WorldToCell(_objectToPlace.GetStartPosition());
						TakeArea(start, _objectToPlace.Size);
						_objectToPlace = null;
					}
					else
					{
						Destroy(_objectToPlace.gameObject);
					}
				}
			}
		}
		public static Vector3 GetMouseWorldPosition()
		{
			_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(_ray, out RaycastHit raycastHit))
			{
				return raycastHit.point;
			}
			else
			{
				return Vector3.zero;
			}
		}

		public Vector3 SnapCoordinateToGrid(Vector3 position)
		{
			Vector3Int cellPos = GridLayout.WorldToCell(position);
			position = _grid.GetCellCenterWorld(cellPos);
			return position;
		}

		private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
		{
			TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
			int counter = 0;

			foreach (Vector3Int areaPosition  in area.allPositionsWithin)
			{
				Vector3Int position = new Vector3Int(areaPosition.x, areaPosition.y, areaPosition.z);
				array[counter] = tilemap.GetTile(position);

				counter++;
			}

			return array;
		}

		public void InitializeWithObject(GameObject prefab)
		{
			Vector3 position = SnapCoordinateToGrid(GetMouseWorldPosition());

			GameObject obj = Instantiate(prefab, position, Quaternion.identity);

			_objectToPlace = obj.GetComponent<PlaceableObject>();
			
			obj.AddComponent<ObjectDrag>();
		}

		public bool CanBePlaced(PlaceableObject placeableObject)
		{
			BoundsInt area = new BoundsInt();
			area.position = GridLayout.WorldToCell(_objectToPlace.GetStartPosition());
			area.size = placeableObject.Size;

			TileBase[] baseArray = GetTilesBlock(area, _tilemap);

			foreach (TileBase tile in baseArray)
			{
				if (tile == _tileBase)
				{
					_ableToPlace = false;
					return false;
				}
			}

			_ableToPlace = true;
			return true;
		}

		private void TakeArea(Vector3Int start, Vector3Int size)
		{
			_tilemap.BoxFill(start, _tileBase, start.x, start.y, start.x + size.x, start.y + size.y);
		}
	}

}