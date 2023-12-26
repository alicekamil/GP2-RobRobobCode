using UnityEngine;

namespace SpaceGame
{
    public class AsteroidMove : MonoBehaviour
    {
        void Start()
        {
            _speed = Random.Range(_speedMin, _speedMax);
            Destroy(gameObject, 45f);
        }

        void Update()
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;
        }

        private float _speed;

        [SerializeField] private float _speedMin = 2f;
        [SerializeField] private float _speedMax = 12f;
    }
}
