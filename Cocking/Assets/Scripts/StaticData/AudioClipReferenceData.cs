using UnityEngine;

namespace StaticData
{
	[CreateAssetMenu(fileName = "Sound", menuName = "Sounds")]
	public class AudioClipReferenceData : ScriptableObject
	{
		public AudioClip[] Chop;
		public AudioClip[] DeliveryFail;
		public AudioClip[] DeliverySuccess;
		public AudioClip[] FootStep;
		public AudioClip[] ObjectDrop;
		public AudioClip[] ObjectPickUp;
		public AudioClip StoveSizzle;
		public AudioClip[] trash;
		public AudioClip[] warning;

	}
}