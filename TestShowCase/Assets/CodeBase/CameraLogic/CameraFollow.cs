using Cinemachine;
using UnityEngine;

namespace CodeBase.CameraLogic
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField] private float _rotationAngleX;
		[SerializeField] private float _distance;
		[SerializeField] private float _offsetY;
		[SerializeField] private CinemachineVirtualCamera _camera;

		public Transform _following;

		private void LateUpdate()
		{
			if (_following == null)
			{
				return;
			}

			Quaternion rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
			Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();

			_camera.transform.rotation = rotation;
			_camera.transform.position = position;
		}

		public void Follow(GameObject following)
		{
			_camera.m_Follow = following.transform;
		}

		private Vector3 FollowingPointPosition()
		{
			Vector3 followingPosition = _following.position;
			followingPosition.y += _offsetY;

			return followingPosition;
		}
	}
}