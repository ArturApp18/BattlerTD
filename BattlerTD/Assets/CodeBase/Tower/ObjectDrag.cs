using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Inputs;
using UnityEngine;

namespace CodeBase.Tower
{
	public class ObjectDrag : MonoBehaviour
	{
		private Vector3 _offset;
		private IInputService _input;

		private void Awake()
		{
			_input = AllServices.Container.Single<IInputService>();
		}
		
		private void OnMouseDown()
		{
			_offset = transform.position - BuildingHandler.GetMouseWorldPosition();
		}

		private void Update()
		{
			Vector3 position = BuildingHandler.GetMouseWorldPosition() + _offset;
			transform.position = BuildingHandler.Current.SnapCoordinateToGrid(position);
		}
	}
}