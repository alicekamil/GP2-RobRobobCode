using System;
using UnityEngine;

namespace SpaceGame
{
    public enum ParticleType
    {
        None,
        Explosion,
        Smack,
        Bonk
    }
    
    public class ParticleManager : MonoSingleton<ParticleManager>
    {
        public void Spawn(ParticleType type, Vector3 position, Transform overrideParent = null)
        {
            var prefab = GetParticleObject(type);
            if (prefab)
                Instantiate(prefab, position, Quaternion.identity, overrideParent);
        }

        public void SpawnScorePopup(int score, Vector3 position)
        {
            var popup = Instantiate(_scorePopup, position, Quaternion.identity);
            popup.Show(score);
        }
        
        private GameObject GetParticleObject(ParticleType type) => type switch
        {
            ParticleType.None => null,
            ParticleType.Explosion => _explosion,
            ParticleType.Smack => _smack,
            ParticleType.Bonk => _bonk,
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"VfxType not found: {type}"),
        };
        
        [Header("Particles")]
        [SerializeField]
        private GameObject _explosion;
        [SerializeField]
        private GameObject _smack;
        [SerializeField]
        private GameObject _bonk;
        [SerializeField]
        private ScorePopup _scorePopup;
    }
}