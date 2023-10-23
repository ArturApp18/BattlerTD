using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.Timers;
using UnityEngine;

namespace CodeBase.Hero
{
	public class HeroMove : MonoBehaviour
	{
		[SerializeField] private CharacterController _characterController;
		[SerializeField] private float _movementSpeed;
		
		private Camera _camera;
		private IInputService _inputService;
		private ITimerService _timerService;

		public void Construct(ITimerService timerService, IInputService inputService)
		{
			_timerService = timerService;
			_inputService = inputService;
		}
		
		private void Start() =>
			_camera = Camera.main;
		private void Update()
		{
			Vector3 movementVector = Vector3.zero;
			
			if (_inputService?.Axis.sqrMagnitude > 0.001 )
			{
				movementVector = _camera.transform.TransformDirection(_inputService.Axis);
				movementVector.y = 0;
				movementVector.Normalize();
    
				transform.forward = movementVector;
			}
    
			movementVector += Physics.gravity;

			if ( _timerService != null)
				_characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
		}
	}
}
