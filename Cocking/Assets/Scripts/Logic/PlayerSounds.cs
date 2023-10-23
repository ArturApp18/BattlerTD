using System;
using Hero;
using UnityEngine;

namespace Logic
{
	public class PlayerSounds : MonoBehaviour
	{
		[SerializeField] private HeroMove _heroMove;

		private float _footstepTimer;
		private float _footstepTimerMax = 0.1f;

		private void Update()
		{
			_footstepTimer -= Time.deltaTime;
			if (_footstepTimer < 0f)
			{
				_footstepTimer = _footstepTimerMax;

				if (_heroMove.IsWalking())
				{
					float volume = 1f;
					SoundLogic.instance.PlayFooystepSound(_heroMove.transform.position, volume);
				}
			}
		}
	}

}