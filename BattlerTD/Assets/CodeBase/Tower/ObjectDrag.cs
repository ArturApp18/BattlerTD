using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Inputs;
using UnityEngine;

namespace CodeBase.Tower
{
	public class ObjectDrag : MonoBehaviour
	{
		private Vector3 _offset;
		private IBuildingService _buildingService;
		private IInputService _inputService;

		public void Construct(IBuildingService buildingService, IInputService inputService)
		{
			_buildingService = buildingService;
			_inputService = inputService;
		}

		public void StopDraggingCoroutine()
		{
			StopCoroutine(OnDraggingObject());
		}
		public void OnObjectTapped()
		{
			_offset = transform.position - _buildingService.GetMouseWorldPosition();
			StartCoroutine(OnDraggingObject());
		}

		private IEnumerator OnDraggingObject()
		{
			while (_buildingService.IsDraggingObject)
			{
				Vector3 position = _buildingService.GetMouseWorldPosition() + _offset;
				transform.position = _buildingService.SnapCoordinateToGrid(position);
				yield return null;
			}
		}
	}
}