using System.Collections.Generic;
using CodeBase.Infrastructure.Services.Skills;
using UnityEngine;

namespace CodeBase.StaticData
{
	[CreateAssetMenu(fileName = "Skills", menuName = "StaticData/AllSkill")]
	public class AllSkillStaticData : ScriptableObject
	{
		public List<Skill> AllSkills;
	}
}