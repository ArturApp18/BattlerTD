using System;
using UnityEngine;

namespace Hero
{
	public class HeroAnimator : MonoBehaviour
	{
		private static readonly int Walking = Animator.StringToHash("Walking");
		private static readonly int IsWalking = Animator.StringToHash("IsWalking");

		[SerializeField] private Animator _animator;
		[SerializeField] private CharacterController _controller;

		private void Update()
		{
			if (_controller.velocity.magnitude != 0)
			{
				_animator.SetBool(IsWalking, true);
			}
			else
			{
				_animator.SetBool(IsWalking, false);
			}
		}
	}
}