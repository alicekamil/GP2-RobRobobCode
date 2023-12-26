using System;
using UnityEngine;

namespace SpaceGame
{
    public class TurretInteractable : Interactable
    {
        public override bool CanInteract(Interactor interactor)
        {
            bool canDeliver = _taskManager.CheckDeliverTaskItem(interactor.ItemHolder.ItemId);
            // _icon.SetColor(canDeliver ? _goodColor : _badColor);

            return base.CanInteract(interactor) && canDeliver;
        }

        protected override void OnInteractionFinished()
        {
            _currentInteractor.ItemHolder.SetItem(null);
            _taskManager.DeliverTaskItem();
            base.OnInteractionFinished();
        }

        private void Awake()
        {
            _taskManager = GetComponent<TurretTaskManager>();
        }

        private TurretTaskManager _taskManager;
    }
}