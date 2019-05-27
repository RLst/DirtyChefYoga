using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
    [RequireComponent(typeof(AudioSettings))]
	[RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        AudioSource audioSource;
        [SerializeField] List<AudioClip> sounds = new List<AudioClip>();

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayRandomOnce()
        {
            var randomSound = sounds[UnityEngine.Random.Range(0, sounds.Count)];
            if (audioSource.enabled == true)
            {
                audioSource.PlayOneShot(randomSound);
            }
        }

		public void PlaySoundOnce(string soundName)
		{
			foreach (var s in sounds)
			{
				if (s.name == soundName)
				{
					audioSource.PlayOneShot(s);
					return;
				}
			}
			Debug.LogWarning("Sound not found!");
		}

		public void RepeatSound(string soundName)
		{
			foreach (var s in sounds)
			{
				if (s.name == soundName)
				{
					audioSource.loop = true;
					audioSource.Play();
					return;
				}
			}
			Debug.LogWarning("Sound not found!");
		}
    }
}