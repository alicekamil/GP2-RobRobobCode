using UnityEngine;

namespace SpaceGame
{
    public class RailgunVFX : MonoBehaviour
    {
        [ContextMenu("Play VFX")]
        public void Play()
        {
            _chargeUp.Play();
            _chargeSound.Play();
            _beam.transform.localScale = new Vector3(1f, 1f, 0f);
            _beam.transform.localPosition = new Vector3(0, 0, -10);
            LeanTween.delayedCall(gameObject, 2f, () => _fireSound.Play());
            LeanTween.scaleZ(_beam, 1f, 0.3f).setDelay(2f).setEaseOutCubic();
            LeanTween.moveLocalZ(_beam, 60f, 0.2f).setDelay(3.2f);
            // LeanTween.delayedCall(gameObject, 2f, () => )
        }

        [SerializeField]
        private ParticleSystem _chargeUp;
        [SerializeField]
        private GameObject _beam;
        [SerializeField]
        private AudioClipSO _fireSound;
        [SerializeField]
        private AudioClipSO _chargeSound;
    }
}