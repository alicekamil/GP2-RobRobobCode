using System;
using UnityEngine;

namespace SpaceGame
{
    public class RepairInteractable : Interactable
    {
        public event Action Repaired;
        public event Action Exploded;
        public event Action Damaged;
        public bool IsRepaired => _isRepaired;

        public void Repair(bool ignoreEvents = false)
        {
            _isRepaired = true;
            _brokenWall.SetActive(false);
            _wall.SetActive(true);
            IsDisabled = true;
            if (!ignoreEvents)
                Repaired?.Invoke();
        }

        public void Break()
        {
            _isRepaired = false;
            IsDisabled = false;

            _missle.SetActive(true);
            var pos = _missle.transform.localPosition;
            pos.z = -15f;
            _missle.transform.localPosition = pos;
            LeanTween.moveLocalZ(_missle, 0, 0.5f).setEaseInQuad();
            LeanTween.delayedCall(0.4875f, () =>
            {
                if (_wall != null)
                {
                    _wall.SetActive(false);
                    _brokenWall.SetActive(true);
                    _missle.SetActive(false);
                }

                ParticleManager.Instance.Spawn(ParticleType.Explosion, _effectOrigin.position);
            });
            Damaged?.Invoke();
            _breakSound.Play();
        }

        public override bool CanInteract(Interactor interactor)
        {
            return base.CanInteract(interactor) && interactor.ItemHolder.ItemId == "wrench";
        }

        protected override void OnInteractionFinished()
        {
            Repair();
            base.OnInteractionFinished();
        }

        private void Awake()
        {
            Repair(true);
        }

        [SerializeField] private bool _isRepaired;
        [SerializeField] private GameObject _wall;
        [SerializeField] private GameObject _brokenWall;
        [Header("Effects")]
        [SerializeField] private Transform _effectOrigin;
        [SerializeField] private AudioClipSO _breakSound;
        [SerializeField] private GameObject _missle;
        
        private float _timer;
        private Material _defaultMaterial;
    }
}