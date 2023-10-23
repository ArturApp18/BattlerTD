using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Skills;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
	public class SkillPanel : WindowBase
	{
		[SerializeField] private int _skillCount;
		
		public List<RectTransform> _skillsPosition;
		public List<SkillTypeId> _SkillsUis;

		private ISkillService _skillService;
		private IUIFactory _uiFactory;

		public void Construct(ISkillService skillService, IPersistentProgressService progressService, ITimerService timerService)
		{
			base.Construct(progressService, timerService);
			_skillService = skillService;

			InitUISkill();
		}

		protected override void OnAwake() =>
			_uiFactory = AllServices.Container.Single<IUIFactory>();

		private void InitUISkill()
		{
			Skill skill = _skillService.TakeSkill();
			
			int i = 0;

			while (i <= _skillCount)
			{
				if (!_SkillsUis.Contains(skill.SkillTypeId) && !_skillService.CurrentSkills.Contains(skill.SkillTypeId))
				{
					SkillUI skillUI = _uiFactory.CreateSkillUI(skill.SkillTypeId, _skillsPosition[i], transform);
					_SkillsUis.Add(skillUI.SkillTypeId);
					CloseButton.Add(skillUI._skillButton);
					skillUI._skillButton.onClick.AddListener(skillUI.TakeSkill);
					i++;
				}
				else if (_SkillsUis.Contains(skill.SkillTypeId) || _skillService.CurrentSkills.Contains(skill.SkillTypeId))
				{
					skill = _skillService.TakeSkill();
				}
				else
				{
					SkillUI skillUI = _uiFactory.CreateSkillUI(SkillTypeId.Default, _skillsPosition[i], transform);
					skillUI._skillButton.onClick.AddListener(skillUI.TakeSkill);
					i++;
				}
			}

			_skillService.InitSkill(skill);
			Debug.Log(i);
			base.OnAwake();
		}

	}

}