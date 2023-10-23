using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Infrastructure.Services.Skills
{

	public abstract class Skill : MonoBehaviour
	{
		protected IPersistentProgressService _progressService;
		public SkillTypeId SkillTypeId;
		protected int _layerMask;

		private void Awake()
		{
			_layerMask = 1 << LayerMask.NameToLayer("Enemy");
		}

		public void Construct(IPersistentProgressService progressService)
		{
			_progressService = progressService;
		}
	}

}