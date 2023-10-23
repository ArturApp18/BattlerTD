using System;
using UnityEngine;

namespace Counters
{
	public class CuttingCounterVisual : MonoBehaviour
	{
		private static readonly int Cut = Animator.StringToHash("Cut");
		
		[SerializeField] private Animator _animator;
		[SerializeField] private CuttingCounter _cuttingCounter;

		private void Start() =>
			_cuttingCounter.OnCut += CuttingCounterOnCut; 

		private void CuttingCounterOnCut(object sender, EventArgs e) =>
			_animator.SetTrigger(Cut);
	}

}