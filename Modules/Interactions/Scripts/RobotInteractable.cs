using UnityEngine;

namespace SpaceGame
{
    public class RobotInteractable : Interactable
    {
        public static RobotInteractable _instance;
        
        public override bool CanInteract(Interactor interactor)
        {
            return base.CanInteract(interactor) && interactor.CharacterType == CharacterType.Human &&
                   interactor.ItemHolder.ItemId == "battery";
        }

        protected override void OnInteractionFinished()
        {
            _currentInteractor.ItemHolder.SetItem(null);
            _batteryLevel = 100f;
            if (_icon.Active)
                _icon.SetActive(false);
            base.OnInteractionFinished();
        }

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            _characterMovement = GetComponent<CharacterMovement>();
            _normalSpeed = _characterMovement.Speed;
            _batteryLevel = 100f;
        }

        private void Update()
        {
            base.Update();
            _batteryLevel = Mathf.Clamp(_batteryLevel, 0, 100);

            if (_batteryLevel > 0f)
            {
                _batteryLevel -= Time.deltaTime * _batteryDrain;
            }
            else
            {
                if (!_icon.Active)
                    _icon.SetActive(true);
            }

            _batteryBar.Progress = _batteryLevel / 100f;
            _characterMovement.Speed = _batteryLevel > 0 ? _normalSpeed : _normalSpeed * _slowModifier;
        }

        public void ReduceBattery()
        {
            _batteryLevel = 0;
        }

        public float _batteryLevel;
        [SerializeField] private float _batteryDrain;
        [SerializeField] private float _slowModifier;
        [SerializeField] private ProgressBar _batteryBar;

        private CharacterMovement _characterMovement;
        private float _normalSpeed;
    }
}