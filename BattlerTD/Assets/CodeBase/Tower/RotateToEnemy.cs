using UnityEngine;

namespace CodeBase.Tower
{
	public class RotateToEnemy : MonoBehaviour
	{
		[SerializeField] private CheckClosestTarget _checkClosestTarget;

		[SerializeField] private float Speed;
    
		private Vector3 _positionToLook;
    
		private void Update()
		{
			if (_checkClosestTarget.Target)
				RotateTowardsHero();
		}
    
		private void RotateTowardsHero()
		{
			UpdatePositionToLookAt();

			transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
		}

		private void UpdatePositionToLookAt()
		{
			Vector3 positionDelta = _checkClosestTarget.Target.position - transform.position;
			_positionToLook = new Vector3(positionDelta.x, transform.position.y, positionDelta.z);
		}

		private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
			Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

		private Quaternion TargetRotation(Vector3 position) =>
			Quaternion.LookRotation(position);

		private float SpeedFactor() =>
			Speed * Time.deltaTime;
	}
}