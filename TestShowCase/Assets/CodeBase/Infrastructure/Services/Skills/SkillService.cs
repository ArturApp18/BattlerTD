using System.Collections.Generic;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Skills
{
	public class SkillService : ISkillService
	{
		private readonly IStaticDataService _staticData;
		private readonly IRandomService _randomService;
		private readonly IPersistentProgressService _progressService;

		private List<Skill> _allSkills = new List<Skill>();

		public SkillService(IStaticDataService staticData, IRandomService randomService, IPersistentProgressService progressService)
		{
			_staticData = staticData;
			_randomService = randomService;
			_progressService = progressService;
		}

		public List<SkillTypeId> CurrentSkills { get; } = new List<SkillTypeId>();

		public void InitAllSkills()
		{
			AllSkillStaticData allSkillData = _staticData.ForAllSkills();
			foreach (Skill skill in allSkillData.AllSkills)
			{
				_allSkills.Add(skill);
			}
		}

		public GameObject CreateSkill(SkillTypeId typeId, Transform parent)
		{
			SkillStaticData skillStaticData = _staticData.ForSkill(typeId);
			GameObject skill = Object.Instantiate(skillStaticData.SkillPrefab, parent.position, Quaternion.identity, parent);
			skill.GetComponent<Skill>().Construct(_progressService);
			//_currentSkills.Add(skill.gameObject.GetComponent<Skill>());
			return skill;
		}

		public Skill TakeSkill() =>
			_allSkills[_randomService.Next(0, _allSkills.Count)];

		public void UpgradeSkill()
		{
			
		}

		public void InitSkill(Skill skill)
		{
			CurrentSkills.Add(skill.SkillTypeId);
		}

	}

}