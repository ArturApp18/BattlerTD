using System;
using UnityEngine;

namespace CodeBase.Tower
{
	public class WallTrapTransform : MonoBehaviour
	{
		[SerializeField] private float _rayLength = 5.0f;
		[SerializeField] private LayerMask _layerMask;
		[SerializeField] private Transform _visual;
		[SerializeField] private float _offset;
		[SerializeField] private float _sideMultiplier;

		private Ray _backwardRay;
		private Ray _forwardRay;
		private Ray _leftRay;
		private Ray _rightRay;

		private void Update()
		{
			
			Vector3 rayOrigin = transform.position;

			_forwardRay = new Ray(rayOrigin, transform.forward);
			RaycastHit forwardHit;

			_backwardRay = new Ray(rayOrigin, -transform.forward);
			RaycastHit backwardHit;

			_leftRay = new Ray(rayOrigin, -transform.right);
			RaycastHit leftHit;

			_rightRay = new Ray(rayOrigin, transform.right);
			RaycastHit rightHit;

			if (Physics.Raycast(_forwardRay, out forwardHit, _rayLength * _sideMultiplier, _layerMask))
			{
				Vector3 offset = (forwardHit.point - _forwardRay.origin).normalized * _offset;
				Vector3 newPosition = forwardHit.point - offset;
				_visual.transform.LookAt(forwardHit.point);
				_visual.transform.position = newPosition;
			}

			if (Physics.Raycast(_backwardRay, out backwardHit, _rayLength * _sideMultiplier, _layerMask))
			{
				Vector3 offset = (backwardHit.point - _backwardRay.origin).normalized * _offset;
				Vector3 newPosition = backwardHit.point - offset;
				_visual.transform.LookAt(backwardHit.point);
				_visual.transform.position = newPosition;
			}


			if (Physics.Raycast(_leftRay, out leftHit, _rayLength * _sideMultiplier, _layerMask))
			{
				Vector3 offset = (leftHit.point - _leftRay.origin).normalized * _offset;
				Vector3 newPosition = leftHit.point - offset;
				_visual.transform.LookAt(leftHit.point);
				_visual.transform.position = newPosition;
			}

			if (Physics.Raycast(_rightRay, out rightHit, _rayLength * _sideMultiplier, _layerMask))
			{
				Vector3 offset = (rightHit.point - _rightRay.origin).normalized * _offset;
				Vector3 newPosition = rightHit.point - offset;
				_visual.transform.LookAt(rightHit.point);
				_visual.transform.position = newPosition;
			}

			Debug.DrawRay(rayOrigin, transform.forward * (_rayLength * _sideMultiplier), Color.green);
			Debug.DrawRay(rayOrigin, -transform.forward * (_rayLength * _sideMultiplier), Color.blue);
			Debug.DrawRay(rayOrigin, -transform.right * (_rayLength * _sideMultiplier), Color.red);
			Debug.DrawRay(rayOrigin, transform.right * (_rayLength *_sideMultiplier), Color.yellow);
		}
	}
}