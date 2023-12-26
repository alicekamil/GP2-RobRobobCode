using UnityEngine;

namespace SpaceGame
{
    public class CharacterInput : MonoBehaviour
    {
        public void SetPlayerIndex(int index)
        {
            string indexName = (index + 1).ToString();
            _horizontalAxis = "Horizontal" + indexName;
            _horizontalAxisJoy = "HorizontalJoy" + indexName;
            _verticalAxis = "Vertical" + indexName;
            _verticalAxisJoy = "VerticalJoy" + indexName;
            _interactAxis = "Interact" + indexName;
        }

        public Vector2 GetAxis()
        {
            Vector2 axis;
            axis.x = Input.GetAxisRaw(_horizontalAxis) + Input.GetAxisRaw(_horizontalAxisJoy);
            axis.y = Input.GetAxisRaw(_verticalAxis) - Input.GetAxisRaw(_verticalAxisJoy);
            return axis.normalized;
        }

        public bool IsInteractionPressed() => Input.GetButtonDown(_interactAxis);
        public bool IsInteractionReleased() => Input.GetButtonUp(_interactAxis);

        private void Awake()
        {
            SetPlayerIndex(0);
        }

        private string _horizontalAxis;
        private string _horizontalAxisJoy;
        private string _verticalAxis;
        private string _verticalAxisJoy;
        private string _interactAxis;

        private int _playerIndex;
    }
}