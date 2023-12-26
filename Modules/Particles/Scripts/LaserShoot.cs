using UnityEngine;

namespace SpaceGame
{
    public class LaserShoot : MonoBehaviour
    {
        private void Awake()
        {
            _lineRendered.enabled = false;
            _sparkParticle.Stop();
            _glowParticle.Stop();
            _ovalParticle.Stop();
        }

        public void PlayParticle()
        {
            _lineRendered.enabled = true;
            _sparkParticle.Play();
            _glowParticle.Play();
            _ovalParticle.Play();
        }

        public void StopParticle()
        {
            _lineRendered.enabled = false;
            _sparkParticle.Stop();
            _glowParticle.Stop();
            _ovalParticle.Stop();
        }

        [SerializeField] private LineRenderer _lineRendered;
        [SerializeField] private ParticleSystem _sparkParticle;
        [SerializeField] private ParticleSystem _glowParticle;
        [SerializeField] private ParticleSystem _ovalParticle;
    }
}