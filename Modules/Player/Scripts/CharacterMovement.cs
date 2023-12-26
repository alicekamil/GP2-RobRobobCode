using UnityEngine;

namespace SpaceGame
{
    public class CharacterMovement : MonoBehaviour
    {
        public float CurrentSpeed => _currentDirection.magnitude;

        public void SetMovementLock(bool locked)
        {
            _isMovementLocked = locked;
            if (locked)
                _targetDirection = Vector2.zero;
        }

        public void Move(Vector2 direction)
        {
            if (_isMovementLocked)
                return;
            _targetDirection = direction;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            // Handle movement
            _currentDirection = Vector3.Lerp(_currentDirection, _targetDirection, _smooth * Time.deltaTime);

            // Wall steering
            if (_targetDirection != Vector2.zero)
            {
                _currentDirection = GetWallSteerDirection(_currentDirection);
            }

            _rb.velocity = new Vector3(_currentDirection.x, 0, _currentDirection.y) * Speed;

            // Handle rotation
            if (_targetDirection != Vector2.zero)
            {
                var targetRotation = Quaternion.LookRotation(new Vector3(_targetDirection.x, 0, _targetDirection.y));
                transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation,
                    Time.deltaTime * _rotationSmooth);
            }
        }

        private Vector2 GetWallSteerDirection(Vector3 current)
        {
            Vector2 steerDirection = current;
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out var hit, 0.75f,
                    _wallMask))
            {
                Vector3 right = Quaternion.AngleAxis(90, Vector3.up) * hit.normal;
                bool wallRight = Physics.Raycast(hit.point + right * _wallSteerOffset + hit.normal * 0.1f, -hit.normal,
                    0.5f,
                    _wallMask);
                bool wallLeft = Physics.Raycast(hit.point - right * _wallSteerOffset + hit.normal * 0.1f, -hit.normal,
                    0.5f,
                    _wallMask);
                Debug.DrawRay(hit.point + right * _wallSteerOffset, -hit.normal, wallRight ? Color.red : Color.green);
                Debug.DrawRay(hit.point - right * _wallSteerOffset, -hit.normal, wallLeft ? Color.red : Color.green);
                // Debug.Log($"Left [{wallLeft}] Right [{wallRight}]");
                if (wallRight && !wallLeft)
                {
                    steerDirection = -new Vector2(right.x, right.z);
                }
                else if (wallLeft && !wallRight)
                {
                    steerDirection = new Vector2(right.x, right.z);
                }
            }

            return Vector2.Lerp(current, steerDirection, 0.8f);
        }

        public float Speed = 4f;
        [SerializeField] private float _smooth = 24f;
        [SerializeField] private float _rotationSmooth = 16f;
        [SerializeField] private LayerMask _wallMask;
        [SerializeField] private float _wallSteerOffset = 0.5f;

        private Vector2 _targetDirection;
        private Vector2 _currentDirection;
        private bool _isMovementLocked;
        private Rigidbody _rb;
    }
}