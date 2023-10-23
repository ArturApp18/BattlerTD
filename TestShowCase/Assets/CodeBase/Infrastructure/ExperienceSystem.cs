using System;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class ExperienceSystem : MonoBehaviour
	{
		public event Action<int> OnLevelUp;

		public int CurrentLevel { get; private set; }
		public int CurrentExperience { get; private set; }

		public int ExperienceRequiredForNextLevel => CalculateExperienceRequired(CurrentLevel + 1);

		private void Start()
		{
			CurrentLevel = 1;
			CurrentExperience = 0;
		}

		public void GainExperience(int amount)
		{
			CurrentExperience += amount;

			if(CurrentExperience >= ExperienceRequiredForNextLevel)
			{
				LevelUp();
			}
		}

		private void LevelUp()
		{
			CurrentLevel++;
			CurrentExperience -= ExperienceRequiredForNextLevel;
			// Вызываем событие о прокачке
			OnLevelUp?.Invoke(CurrentLevel);
		}

		private int CalculateExperienceRequired(int level)
		{
			// Здесь может быть ваша формула для расчета опыта
			return level * 100;
		}
	}
}