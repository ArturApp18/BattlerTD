using UnityEngine;

namespace CodeBase.Tower
{
	public class ArrowHandler : MonoBehaviour
	{
		[SerializeField] private Arrow _arrow;

		public void PushArrow(Transform enemyTransform)
		{
			Arrow arrow = Instantiate(_arrow, transform.position, Quaternion.identity);
			if (enemyTransform.position.x > transform.position.x)
			{
				arrow.transform.Rotate(0,-90,0);
			}
			else
			{
				arrow.transform.Rotate(0,90,0);
			}
		}
	}
}