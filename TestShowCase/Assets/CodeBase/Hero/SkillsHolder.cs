using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Skills;
using UnityEngine;

namespace CodeBase.Hero
{
	public class SkillsHolder : MonoBehaviour, ISavedProgress
	{
		public List<Skill> Skills;
		public void LoadProgress(PlayerProgress progress)
		{
			throw new System.NotImplementedException();
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			throw new System.NotImplementedException();
		}

		public void Construct()
		{
			
		}
	}
}