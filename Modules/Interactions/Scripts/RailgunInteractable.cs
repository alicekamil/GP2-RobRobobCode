
using UnityEngine;

namespace SpaceGame
{
    public class RailgunInteractable : Interactable
    {
        public override bool CanInteract(Interactor interactor)
        {
            return base.CanInteract(null);
        }

        protected override void OnInteractionFinished()
        {
            base.OnInteractionFinished();
            _railgunManager._readyToFire = false;
            _railgunManager._timer = 0f;
        }
        
        [SerializeField] private RailgunManager _railgunManager;

    }
}