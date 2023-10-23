using UnityEngine;

namespace CodeBase.Enemy
{
  public class RotateToHero : Follow
  {
    public float Speed;

    public Transform _targetTransform;
    public Transform _targetCachedTransform;
    private Vector3 _positionToLook;

    public void Construct(Transform pumpkinTransform)
      => _targetTransform = pumpkinTransform;
    
    private void Update()
    {
      if (_targetTransform)
        RotateTowardsHero();
    }

    public void ChangeToOldTarget() =>
      _targetTransform = _targetCachedTransform;

    public void ChangeToNewTarget(Transform heroTransform) =>
      _targetTransform = heroTransform;

    private void RotateTowardsHero()
    {
      UpdatePositionToLookAt();

      transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
    }

    private void UpdatePositionToLookAt()
    {
      Vector3 positionDelta = _targetTransform.position - transform.position;
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