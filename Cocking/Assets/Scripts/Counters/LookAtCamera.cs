using UnityEngine;

namespace Counters
{
	public class LookAtCamera : MonoBehaviour
	{
		private enum Mode
		{
			LookAt = 0,
			LookAtInverted = 1,
			CameraForward = 2,
			CameraForwardInverted = 3,
		}

		[SerializeField] private Mode _mode;

		private void LateUpdate()
		{
			switch (_mode)
			{
				case Mode.LookAt:
					transform.LookAt(Camera.main.transform);
					break;
				case Mode.LookAtInverted:
					Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
					transform.LookAt(transform.position + dirFromCamera);
					break;
				case Mode.CameraForward:
					transform.forward = Camera.main.transform.forward;
					break;
				case Mode.CameraForwardInverted:
					transform.forward = -Camera.main.transform.forward;
					break;
			}

			transform.LookAt(Camera.main.transform);
		}
	}

}