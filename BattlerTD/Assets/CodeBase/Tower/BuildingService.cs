using System;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace CodeBase.Tower
{
	public class BuildingService : IBuildingService
	{
		private GridLayout _gridLayout;
		private Grid _grid;

		private MeshRenderer[] _ableToPlaceVisuals;
		private Tilemap _tilemap;

		private TileBase _tileWalls;
		private TileBase _tileFloors;

		private GameObject _iceOrb;
		private GameObject _fireOrb;

		private PlaceableObject _objectToPlace;
		private readonly IInputService _input;
		private readonly IGameFactory _factory;
		private readonly IStaticDataService _staticData;
		private bool _ableToPlace;
		private bool _isActive;
		private bool _isDraggingObject;
		private ObjectDrag _drag;
		private int _layerMask;
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
		public GridLayout GridLayout
		{
			get
			{
				return _gridLayout;
			}
		}
		public bool IsDraggingObject
		{
			get
			{
				return _isDraggingObject;
			}
		}

		public BuildingService(IInputService input, IGameFactory factory, IStaticDataService staticData)
		{
			_layerMask = 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Wall");
			_input = input;
			_factory = factory;
			_staticData = staticData;
			_input.TowerButtonPressed += InitTowerType;
			_input.TowerButtonUnpressed += TowerButtonUnpressed;
		}

		public void Init()
		{
			_grid = _factory.CreateGrid();
			_gridLayout = _grid.GetComponent<GridLayout>();
			_tilemap = _grid.GetComponentInChildren<Tilemap>();
		}

		private void TowerButtonUnpressed()
		{
			_isDraggingObject = false;
			_drag.StopDraggingCoroutine();
			if (CanBePlaced(_objectToPlace, _objectToPlace.InAnotherTower))
			{
				_objectToPlace.Place(_drag);
				Vector3Int start = GridLayout.WorldToCell(_objectToPlace.GetStartPosition());
				TakeArea(start, _objectToPlace.Size);
				_objectToPlace = null;
			}
			else
			{
				Object.Destroy(_objectToPlace.gameObject);
			}
		}

		public Vector3 GetMouseWorldPosition()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit raycastHit, _layerMask))
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

		public bool CanBePlaced(PlaceableObject placeableObject, bool inAnotherCollider)
		{
			if (inAnotherCollider)
			{
				_ableToPlace = false;
			}
			else
			{
				BoundsInt area = new BoundsInt();

				area.position = GridLayout.WorldToCell(_objectToPlace.GetStartPosition());
				area.size = placeableObject.Size;

				TileBase[] baseArray = GetTilesBlock(area, _tilemap);

				foreach (TileBase tile in baseArray)
				{
					Debug.Log(tile);
					if (tile == placeableObject.TileBaseAfterPlaced || tile == null || tile != placeableObject.TileBase)
					{
						_ableToPlace = false;
						break;
					}
					else
					{
						_ableToPlace = true;
					}
				}
			}

			
			Debug.Log($"able+ {_ableToPlace}");
			Debug.Log($"collider+ {inAnotherCollider}");
			return _ableToPlace;
		}

		private void InitTowerType()
		{
			if (!_isActive)
				return;

			if (_input.IsTurretTowerButtonDown())
			{
				InitializeWithObject(TowerType.WallArrowTrap);
			}
			else if (_input.IsOrbLightningButtonDown())
			{
				InitializeWithObject(TowerType.LightningOrb);
			}
			else if (_input.IsWallArrowTrapTowerButtonDown())
			{
				InitializeWithObject(TowerType.LightningOrb);
			}
			else if (_input.IsFloorTrapTowerButtonDown())
			{
				InitializeWithObject(TowerType.LightningOrb);
			}
		}

		private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
		{
			TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];

			int counter = 0;

			foreach (Vector3Int areaPosition in area.allPositionsWithin)
			{
				Vector3Int position = new Vector3Int(areaPosition.x, areaPosition.y, 0);
				array[counter] = tilemap.GetTile(position);

				counter++;
			}

			return array;
		}

		private GameObject InitTower(TowerType towerType, Vector3 position) =>
			_factory.CreateTower(towerType, position, Quaternion.identity);

		private void InitializeWithObject(TowerType towerType)
		{
			Vector3 position = SnapCoordinateToGrid(GetMouseWorldPosition());

			_objectToPlace = InitTower(towerType, position).GetComponent<PlaceableObject>();

			RegisterDrag();
		}

		private void RegisterDrag()
		{
			_objectToPlace.Construct(this);
			_isDraggingObject = true;
			_drag = _objectToPlace.GetComponent<ObjectDrag>();
			_drag.Construct(this, _input);
			_drag.OnObjectTapped();
		}

		private void TakeArea(Vector3Int start, Vector3Int size) =>
			_tilemap.BoxFill(start, _objectToPlace.TileBaseAfterPlaced, start.x, start.y, start.x + size.x, start.y + size.y);
	}

}