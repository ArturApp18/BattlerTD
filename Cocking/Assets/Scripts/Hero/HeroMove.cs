using Infrastructure;
using Services.Input;
using UnityEngine;

namespace Hero
{
	public class HeroMove : MonoBehaviour
	{
		[SerializeField] private CharacterController _characterController;
		[SerializeField] private float movementSpeed;

		private IInputService _inputService;
		private Camera _camera;

		private void Awake()
		{
			_inputService = Game.InputService;
		}

		private void Start()
		{
			_camera = Camera.main;
		}

		private void Update()
		{
			Vector3 movementVector = Vector3.zero;

			if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
			{
				movementVector = _camera.transform.TransformDirection(_inputService.Axis);
				movementVector.y = 0;
				movementVector.Normalize();

				transform.forward = movementVector;
			}
			
			movementVector += Physics.gravity;
			_characterController.Move(movementVector * movementSpeed * Time.deltaTime);
		}

		public bool IsWalking()
		{
			return _characterController.velocity.magnitude != 0;
		}
	}
}