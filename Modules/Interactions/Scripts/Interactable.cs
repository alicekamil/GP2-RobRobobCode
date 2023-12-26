using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class Interactable : MonoBehaviour
    {
        public static List<Interactable> Interactables = new();
        public float Range => _range;
        public bool IsDisabled
        {
            get => _isDisabled;
            set
            {
                // Hide icon on disable
                if (_icon != null)
                    _icon.SetActive(!value);
                _isDisabled = value;
            }
        }

        public virtual bool CanInteract(Interactor interactor) =>
            !_isDisabled && (_currentInteractor == null || _currentInteractor == interactor);

        public void Enable() => _isDisabled = false;
        public void Disable() => _isDisabled = true;

        public void AddInteractor()
        {
            _availableInteractorsCount++;
            UpdateBubble();
        }

        public void RemoveInteractor()
        {
            _availableInteractorsCount--;
            UpdateBubble();
        }

        public void Interact(Interactor interactor)
        {
            _currentInteractor = interactor;
            OnInteractionStarted();
        }

        public void Cancel(Interactor interactor)
        {
            OnInteractionCanceled();
            _currentInteractor = null;
        }

        protected virtual void OnInteractionStarted()
        {
            // This happens when we start the interaction
            _interactionTimer = 0;
        }

        protected virtual void OnInteractionFinished()
        {
            // This happens when we complete the interaction
            _currentInteractor.FinishInteraction();
            _currentInteractor = null;
            _interactSoundClip?.Play();
            _bubble.SetProgress(0f);
        }

        protected virtual void OnInteractionCanceled()
        {
            // Happens when we cancel the interaction
            _bubble.SetProgress(0);
        }

        protected virtual void OnInteractionUpdate()
        {
            // Happens every frame while interacting
            _interactionTimer += Time.deltaTime;

            if (_interactionTimer >= _duration)
            {
                OnInteractionFinished();
            }

            float progress = Mathf.Clamp01(_interactionTimer / _duration);
            _bubble.SetProgress(progress);
        }

        private void UpdateBubble()
        {
            if (_availableInteractorsCount > 0)
            {
                _bubble.SetActive(true);
            }
            else
            {
                _bubble.SetActive(false);
            }
        }

        private void Awake()
        {
            if (_icon != null && IsDisabled)
                _icon.SetActive(false);
        }

        private void OnEnable() => Interactables.Add(this);
        private void OnDisable() => Interactables.Remove(this);

        protected virtual void Update()
        {
            if (_currentInteractor != null)
            {
                OnInteractionUpdate();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _range);
        }

        [SerializeField] private float _range = 1f;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private InteractableBubble _bubble;
        [SerializeField] private bool _isDisabled;
        [SerializeField] private AudioClipSO _interactSoundClip;
        [SerializeField] protected InteractableIcon _icon;

        protected Interactor _currentInteractor;
        private int _availableInteractorsCount;
        private float _interactionTimer;
    }
}