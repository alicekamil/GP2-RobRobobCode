using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    [CreateAssetMenu(menuName = "Audio/Clip")]
    public class AudioClipSO : ScriptableObject
    {
        public void Play()
        {
          AudioManager.Instance.PlaySound(_clip, _volume);
        }

        [SerializeField] private AudioClip _clip;
        [SerializeField, Min(0)] private float _volume;

    }

}