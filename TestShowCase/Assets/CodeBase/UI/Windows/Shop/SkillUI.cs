using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Skills;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
	public class SkillUI : MonoBehaviour
	{
		public Button _skillButton;

		public SkillTypeId SkillTypeId;
		public TextMeshProUGUI Name;
		public TextMeshProUGUI Level;
		public TextMeshProUGUI Description;
		public Image SkillImage;

		private ISkillService _skillService;
		private IGameFactory _gameFactory;
		private IStaticDataService _staticData;

		private void Awake()
		{
			_skillService = AllServices.Container.Single<ISkillService>();
			_gameFactory = AllServices.Container.Single<IGameFactory>();
			_staticData = AllServices.Container.Single<IStaticDataService>();
		}

		public void TakeSkill()
		{
			if (_skillService != null)
			{
				_skillService.CreateSkill(SkillTypeId, _gameFactory.HeroGameObject.transform);
			}
		}
	}
}