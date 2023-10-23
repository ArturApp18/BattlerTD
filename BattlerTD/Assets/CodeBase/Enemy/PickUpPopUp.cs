using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class PickUpPopUp : MonoBehaviour
	{
		private static readonly int PickUp = Animator.StringToHash("PickUp");

		[SerializeField] private Animator _pickUpAnimator;
		[SerializeField] private TextMeshPro _damageText;

		public TextMeshPro DamageText => _damageText;

		public void PlayPopUp()
		{
			_pickUpAnimator.SetTrigger(PickUp);
		}

	}
}