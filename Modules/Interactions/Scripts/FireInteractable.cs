namespace SpaceGame
{
    /// <summary>
    /// Fires have this interactable, only the Robot can use it.
    /// Plays a specific animation when interacting.
    /// </summary>
    public class FireInteractable : Interactable
    {
        public override bool CanInteract(Interactor interactor)
        {
            return base.CanInteract(interactor) && interactor.CharacterType == CharacterType.Robot;
        }

        protected override void OnInteractionStarted()
        {
            base.OnInteractionStarted();
            _currentInteractor.SetAnimBool("Extinguish", true);
        }

        protected override void OnInteractionFinished()
        {
            _currentInteractor.SetAnimBool("Extinguish", false);
            _destroyFire.DestroySelf();
            base.OnInteractionFinished();
        }

        protected override void OnInteractionCanceled()
        {
            _currentInteractor.SetAnimBool("Extinguish", false);
            base.OnInteractionCanceled();
        }

        private void Awake()
        {
            _destroyFire = GetComponent<DestroyFire>();
        }

        private DestroyFire _destroyFire;
    }
}