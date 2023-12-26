using System;
using UnityEngine;

namespace SpaceGame
{
    // This class will interact with interactables
    public class Interactor : MonoBehaviour
    {
        public event Action InteractionStarted;
        public event Action InteractionFinished;

        public bool CanInteract => _closestInteractable != null;
        public bool CanDropItem => _itemHolder.ItemId != null;
        public ItemHolder ItemHolder => _itemHolder;
        public CharacterType CharacterType => _character.CharacterType;

        public void Setup(ItemHolder itemHolder, CharacterLogic character)
        {
            _itemHolder = itemHolder;
            _character = character;
        }

        public void Interact()
        {
            _closestInteractable.Interact(this);
            _currentInteractable = _closestInteractable;
            InteractionStarted?.Invoke();
        }

        public void CancelInteract()
        {
            // Cancel current interaction
            if (_currentInteractable != null)
            {
                _currentInteractable.Cancel(this);
                InteractionFinished?.Invoke();
                _currentInteractable = null;
            }
        }

        public void DropItem()
        {
            var itemId = _itemHolder.ItemId;
            _itemHolder.SetItem(null);
            var itemParent = _itemHolder.ItemParent;
            var droppedItem = Instantiate(_itemPrefab, itemParent.position, itemParent.rotation);
            droppedItem.SetItem(itemId);

            Vector3 velocity = (itemParent.forward + Vector3.up * 1f) * _throwForce;

            if (CharacterType == CharacterType.Human)
            {
                velocity += _rb.velocity;
            }

            droppedItem.Throw(velocity);
        }

        public void FinishInteraction()
        {
            InteractionFinished?.Invoke();
            _currentInteractable = null;
        }

        public void SetAnimBool(string name, bool value)
        {
            _character.SetAnimBool(name, value);
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var newInteractable = FindClosestInteractable();

            if (newInteractable != null)
            {
                if (newInteractable != _closestInteractable)
                {
                    if (_closestInteractable != null)
                        _closestInteractable.RemoveInteractor();
                    newInteractable.AddInteractor();
                }
            }
            else
            {
                if (_closestInteractable != null)
                    _closestInteractable.RemoveInteractor();
            }

            _closestInteractable = newInteractable;
        }

        private Interactable FindClosestInteractable()
        {
            // Go through every interactable in the game and find the closest available one
            float minDistance = float.MaxValue;
            Interactable closest = null;

            foreach (var interactable in Interactable.Interactables)
            {
                float distance = Vector3.Distance(transform.position, interactable.transform.position);
                if (distance <= interactable.Range && interactable.CanInteract(this) && distance < minDistance)
                {
                    minDistance = distance;
                    closest = interactable;
                }
            }

            return closest;
        }

        [SerializeField] private ItemInteractable _itemPrefab;
        [SerializeField] private float _throwForce;

        private CharacterLogic _character;
        private Interactable _currentInteractable;
        private Interactable _closestInteractable;
        private ItemHolder _itemHolder;
        private Rigidbody _rb;
    }
}