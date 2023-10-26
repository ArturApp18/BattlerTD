using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
	public class MainBuildingAnimator : MonoBehaviour, IAnimationStateReader
	{
		private static readonly int HitHash = Animator.StringToHash("Hit");
    
		[SerializeField] public Animator _animator;
    
		public AnimatorState State { get; }
		
		public void PlayHit()
		{
			_animator.SetTrigger(HitHash);
		}
		public void EnteredState(int stateHash)
		{
      
		}

		public void ExitedState(int stateHash)
		{
      
		}

	}
}