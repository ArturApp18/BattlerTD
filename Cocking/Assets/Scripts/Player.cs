using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _rotateSpeed;
	[SerializeField] private GameInput _gameInput;
	private bool _isWalking;

	private const string Horizontal = "Horizontal";
	private const string Vertical = "Vertical";

	private void Update()
	{
		Vector2 inputVector = _gameInput.GetMovementVectorNormalized();

		Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

		transform.position += moveDir * _moveSpeed * Time.deltaTime;

		_isWalking = moveDir != Vector3.zero;
		
		transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotateSpeed);
	}

	public bool IsWalking() =>
		_isWalking;
}