using System;
using UnityEngine;

namespace CodeBase.UI
{
	public class HudAnimator : MonoBehaviour
	{
		private static readonly int Appear = Animator.StringToHash("Appear");
		
		[SerializeField] private Animator _animator;

		private void Start()
		{
			CutsceneManager.Instance.OnCutsceneEnded += Appearing;
		}

		private void Appearing()
		{
			_animator.SetTrigger(Appear);
		}
	}
}