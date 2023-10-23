using UnityEngine;

namespace CodeBase.Infrastructure.Services.Audio
{
	public class AudioService : IAudioService
	{ 
		private readonly AudioSource _audioSource;

		public AudioService(AudioSource audioSource) =>
			_audioSource = audioSource;

		public void PlaySound(AudioClip clip)
		{
			_audioSource.clip = clip;
			_audioSource.Play();
		}

		public void PlayBackgroundMusic()
		{
			_audioSource.Play();
		}

		public void PauseSound()
		{
			_audioSource.Pause();
		}

		public void ResumeSound()
		{
			_audioSource.UnPause();
		}

		public void StopSound()
		{
			_audioSource.Stop();
		}

		public void SetVolume(float volume)
		{
			_audioSource.volume = volume;
		}
	}

}