using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
	public class ActorUI : MonoBehaviour
	{
		public HpBar HpBar;
		public ExperienceBar ExperienceBar;

		private IPersistentProgressService _progressService;
		private IHealth _health;
		private LootData _lootData;

		public void Construct(IHealth health, IPersistentProgressService progressService)
		{
			_health = health;
			_progressService = progressService;
			_lootData = _progressService.Progress.WorldData.LootData;
			_health.HealthChanged += UpdateHpBar;
			_lootData.Changed += UpdateExperienceBar;
		}

		private void Construct(IHealth health)
		{
			_health = health;
			_health.HealthChanged += UpdateHpBar;
		}

		private void Start()
		{
			IHealth health = GetComponent<IHealth>();

			if (health != null)
				Construct(health);
			
			Debug.Log(health);
		}

		private void OnDestroy()
		{
			if (_health != null)
				_health.HealthChanged -= UpdateHpBar;
		}

		private void UpdateHpBar()
		{
			if (HpBar != null)
				HpBar.SetValue(_health.Current, _health.Max);
		}

		private void UpdateExperienceBar()
		{
			if (ExperienceBar != null)
				ExperienceBar.SetValue(_lootData.Collected, _lootData.RequiredPointForNextLevel);
		}
	}
}