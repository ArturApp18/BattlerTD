using UnityEngine;

namespace CodeBase.StaticData
{
	[CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/Hero")]
	public class HeroStaticData : ScriptableObject
	{
		[Header("HP")]
		[Range(1, 100)]
		public int Hp;

		[Header("Damage")]
		[Range(1, 50)]
		public float Damage;
		[Range(0.5f, 5)]
		public float Cleavage;

		[Header("Movement")]
		[Range(1, 100)]
		public float MaxMoveSpeed;
		
		public GameObject Prefab;
	}
}