using UnityEngine;
using UnityEngine.Events;

namespace SpaceGame
{
    /// <summary>
    /// Manages syncing up two button presses.
    /// State 1 - nobody pressed any buttons (default)
    /// State 2 - one button was pressed, waiting for other button
    /// State 3 - other button was pressed as well, invoke Interacted
    /// </summary>
    public class CoopButtonInteractable : MonoBehaviour
    {
        public UnityEvent Interacted;

        public void Enable()
        {
            _buttonA.IsDisabled = false;
            _buttonB.IsDisabled = false;
        }

        public void Disable()
        {
            _buttonA.IsDisabled = true;
            _buttonB.IsDisabled = true;
        }

        private void Awake()
        {
            _buttonA.Interacted += OnButtonPressed;
            _buttonB.Interacted += OnButtonPressed;
        }

        private void Update()
        {
            if (_isWaitingForButton)
            {
                _timer += Time.deltaTime;
                float progress = _timer / _pressWindow;
                _buttonA.SetTimeWindowProgress(progress);
                _buttonB.SetTimeWindowProgress(progress);
                if (_timer > _pressWindow)
                {
                    FailInteraction();
                }
            }
        }

        private void OnButtonPressed(ButtonInteractable button)
        {
            button.IsButtonDisabled = true;
            if (_isWaitingForButton)
            {
                CompleteInteraction();
            }
            else
            {
                WaitForOtherButton();
            }
        }

        private void WaitForOtherButton()
        {
            _isWaitingForButton = true;
            _timer = 0f;
        }

        private void CompleteInteraction()
        {
            Interacted?.Invoke();
            // Reset buttons
            ResetButtons();
        }

        private void FailInteraction()
        {
            _isWaitingForButton = false;
            // Reset buttons
            ResetButtons();
        }

        private void ResetButtons()
        {
            _buttonA.IsButtonDisabled = false;
            _buttonB.IsButtonDisabled = false;
            _buttonA.SetTimeWindowProgress(0);
            _buttonB.SetTimeWindowProgress(0);
        }

        [SerializeField] private float _pressWindow;
        [SerializeField] private ButtonInteractable _buttonA;
        [SerializeField] private ButtonInteractable _buttonB;
        
        private float _timer;
        private bool _isWaitingForButton;
    }
}