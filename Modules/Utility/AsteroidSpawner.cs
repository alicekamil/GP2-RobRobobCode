using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceGame
{
    public class AsteroidSpawner : MonoBehaviour
    {
        private void SpawnObject()
        {
            _time = _minTime;
            Vector3 randomPos = Random.insideUnitSphere * _radius;

            Instantiate(_asteroid, transform.position + randomPos, Random.rotation);
        }

        private void SetRandomTime()
        {
            _spawnTime = Random.Range(_minTime, _maxTime);
        }

        private void Start()
        {
            SetRandomTime();
            _time = _minTime;
        }

        private void FixedUpdate()
        {
            _time += Time.deltaTime;

            if (_time >= _spawnTime)
            {
                SpawnObject();
                SetRandomTime();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        [SerializeField] private GameObject _asteroid;
        [SerializeField] private float _maxTime = 5;
        [SerializeField] private float _minTime = 2;
        [SerializeField] private float _radius = 2;

        private float _time;
        private float _spawnTime;

        private Vector3 _randomSpawn;
    }
}