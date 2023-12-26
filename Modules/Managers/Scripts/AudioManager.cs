using UnityEngine;

namespace SpaceGame
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public void PlaySound(AudioClip clip, float volume)
        {
            _audioSource.volume = volume;
            _audioSource.PlayOneShot(clip);
        }
        [SerializeField] private AudioSource _audioSource;
       
    }
}