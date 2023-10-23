using System.Collections.Generic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Skills
{
	public interface ISkillService : IService
	{
		GameObject CreateSkill(SkillTypeId typeId, Transform parent);
		Skill TakeSkill();
		void UpgradeSkill();
		void InitAllSkills();
		void InitSkill(Skill skill);
		List<SkillTypeId> CurrentSkills { get; }
	}
}