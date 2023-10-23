using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.StaticData
{
	[CreateAssetMenu(fileName = "SkillData", menuName = "StaticData/Skill")]
	public class SkillStaticData : ScriptableObject
	{
		public SkillTypeId SkillTypeId;
		public GameObject SkillPrefab;
		public GameObject SkillUIPrefab;
		public string Name;
		public string Level;
		public string Description;
		public Sprite SkillImage;
	}

}