using UnityEngine;

namespace SpaceGame
{
    /// <summary>
    /// Converts one item to another
    /// </summary>
    public class ConverterInteractable : Interactable
    {
        public override bool CanInteract(Interactor interactor)
        {
            return base.CanInteract(interactor) && interactor.ItemHolder.ItemId == _sourceItemId;
        }

        protected override void OnInteractionFinished()
        {
            _currentInteractor.ItemHolder.SetItem(_resultItemId);
            base.OnInteractionFinished();
        }

        [SerializeField] private string _sourceItemId;
        [SerializeField] private string _resultItemId;
    }
}