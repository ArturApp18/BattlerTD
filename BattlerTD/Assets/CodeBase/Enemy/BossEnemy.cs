using System;
using System.Reflection.Emit;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class BossEnemy : MonoBehaviour
	{
		public Action BossDefeated;

		public void Construct()
		{
			
		}
		private void OnDestroy()
		{
			BossDefeated?.Invoke();
		}
	}
}