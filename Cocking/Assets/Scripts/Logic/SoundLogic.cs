using System;
using Counters;
using Counters.CountersLogic;
using Hero;
using StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic
{
	public class SoundLogic : MonoBehaviour
	{
		[SerializeField] private AudioClipReferenceData _audioClipReferenceData;
		[SerializeField] private OrderReceiver _orderReceiver;
		[SerializeField] private DeliveryCounter _deliveryCounter;
		public static SoundLogic instance { get; private set; }

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			_orderReceiver.OnRecipeSuccess += OrderReceiverOnRecipeSuccess;
			_orderReceiver.OnRecipeFailed += OrderReceiverOnRecipeFailed;
			CuttingCounter.OnAnyCut += CuttingCounterOnAnyCut;
			BaseCounter.OnAnyObjectPlaced += BaseCounterOnAnyObjectPlaced;
			TrashCounters.OnAnyObjectTrashed += TrashCountersOnAnyObjectTrashed;
			HeroInteractions.OnPicked += HeroInteractionsOnPicked;
		}

		private void TrashCountersOnAnyObjectTrashed(object sender, EventArgs e)
		{
			TrashCounters trashCounters = sender as TrashCounters;
			PlaySound(_audioClipReferenceData.trash, trashCounters.transform.position);
		}

		private void BaseCounterOnAnyObjectPlaced(object sender, EventArgs e)
		{
			BaseCounter baseCounter = sender as BaseCounter;
			PlaySound(_audioClipReferenceData.ObjectDrop, baseCounter.transform.position);
		}

		private void HeroInteractionsOnPicked(object sender, EventArgs e)
		{
			PlaySound(_audioClipReferenceData.ObjectPickUp, HeroInteractions.Instance.transform.position);
		}

		private void CuttingCounterOnAnyCut(object sender, EventArgs e)
		{
			CuttingCounter cuttingCounter = sender as CuttingCounter;
			PlaySound(_audioClipReferenceData.Chop, cuttingCounter.transform.position);
		}

		private void OrderReceiverOnRecipeFailed(object sender, EventArgs e) =>
			PlaySound(_audioClipReferenceData.DeliveryFail, _deliveryCounter.transform.position);

		private void OrderReceiverOnRecipeSuccess(object sender, EventArgs e) =>
			PlaySound(_audioClipReferenceData.DeliverySuccess, _deliveryCounter.transform.position);

		private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f) =>
			PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, volume);

		private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) =>
			AudioSource.PlayClipAtPoint(audioClip, position, volume);

		public void PlayFooystepSound(Vector3 position, float volume)
		{
			PlaySound(_audioClipReferenceData.FootStep, position, volume);
		}
	}
	//Проблема с HeroInteraction скорее всего нужна фабрика время 7:58:52
}