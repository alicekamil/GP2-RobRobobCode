using UnityEngine;
using System;

namespace SpaceGame
{
    /// <summary>
    /// Dropped items that can be picked up
    /// </summary>
    public class ItemInteractable : Interactable
    {
        public string ItemId => _itemHolder.ItemId;

        public void SetItem(string id)
        {
            _itemId = id;
            _itemHolder.SetItem(_itemId);
        }

        public void Throw(Vector3 force)
        {
            _rb.AddForce(force, ForceMode.VelocityChange);
        }

        public override bool CanInteract(Interactor interactor)
        {
            return base.CanInteract(interactor) && interactor.ItemHolder.ItemId == null;
        }

        protected override void OnInteractionFinished()
        {
            _currentInteractor.ItemHolder.SetItem(_itemId);
            base.OnInteractionFinished();
            Destroy(gameObject);
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            if (!String.IsNullOrEmpty(_itemId))
            {
                SetItem(_itemId);
            }
        }

        [SerializeField] private string _itemId;
        [SerializeField] ItemHolder _itemHolder;
        private Rigidbody _rb;
    }
}