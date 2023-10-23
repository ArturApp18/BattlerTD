using UnityEngine;

namespace CodeBase.CameraLogic
{
	[ExecuteInEditMode]
	public class CameraScaler : MonoBehaviour
	{
		public Vector2 DefaultResolution = new Vector2(720, 1280);
		[Range(0, 1)] public float WidthOrHeight = 0;
		[SerializeField] private Camera _camera;
		private float _initialFow;
		private float _horizontalFow;
		private float targetAspect;

		private void Start()
		{
			_camera = Camera.main;

			_initialFow = _camera.fieldOfView;
		}

		private void Update()
		{
			targetAspect = DefaultResolution.x / DefaultResolution.y;
			_horizontalFow = CalculateVerticalFow(_initialFow, 1 / targetAspect);
			float constantWidthFov = CalculateVerticalFow(_horizontalFow, _camera.aspect);
			_camera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFow, WidthOrHeight);
		}

		private float CalculateVerticalFow(float horizontalFowInDeg, float aspectRatio)
		{
			float horizontalInRads = horizontalFowInDeg * Mathf.Deg2Rad;

			float verticalFovInRads = 2 * Mathf.Atan(Mathf.Tan(horizontalInRads / 2) / aspectRatio);

			return verticalFovInRads * Mathf.Rad2Deg;
		}
	}
}