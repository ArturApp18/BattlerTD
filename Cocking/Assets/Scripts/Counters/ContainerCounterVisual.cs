using System;
using UnityEngine;

namespace Counters
{
	public class ContainerCounterVisual : MonoBehaviour
	{
		private static readonly int OpenClose = Animator.StringToHash("OpenClose");
		
		[SerializeField] private Animator _animator;
		[SerializeField] private ContainerCounter _containerCounter;

		private void Start()
		{
			_containerCounter.OnPlayerGrabbedObject += PlayerGrabbedObject;
		}

		private void PlayerGrabbedObject(object sender, EventArgs e)
		{
			_animator.SetTrigger(OpenClose);
		}
	}

}