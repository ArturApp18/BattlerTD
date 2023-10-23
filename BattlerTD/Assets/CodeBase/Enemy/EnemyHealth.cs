using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{

	public class EnemyHealth : MonoBehaviour, IHealth
	{
		public EnemyAnimator Animator;

		[SerializeField] private float _current;

		[SerializeField] private float _max;

		[SerializeField] private PickUpPopUp PickupPopup;
		[SerializeField] private Canvas PickupPopupPrefab;
		
		public event Action HealthChanged;

		public float Current
		{
			get => _current;
			set => _current = value;
		}

		public float Max
		{
			get => _max;
			set => _max = value;
		}

	
		public void ResetPopUp() { }

		public void TakeDamage(float damage)
		{
			Current -= damage;
			
			//PickUpPopUp pickUpPopUp = Instantiate(PickupPopup, PickupPopupPrefab.transform);
			
			//pickUpPopUp.DamageText.text = $"{damage}";

			//pickUpPopUp.PlayPopUp();
			Animator.PlayHit();

			HealthChanged?.Invoke();
		}

		public void LevelUp()
		{
			
		}

	}

}