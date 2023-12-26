using UnityEngine;

namespace SpaceGame
{
    public class FireFlicker : MonoBehaviour
    {
        private void Awake()
        {
            _light = GetComponent<Light>();
            _intensity = Random.Range(_intensityMin, _intensityMax);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _delay)
            {
                _intensity = Random.Range(_intensityMin, _intensityMax);
                _timer = 0f;
            }

            _light.intensity = Mathf.Lerp(_light.intensity, _intensity, Time.deltaTime * 12f);
        }

        [SerializeField] private float _intensityMin;
        [SerializeField] private float _intensityMax;
        [SerializeField] private float _delay = 0.1f;
        private float _intensity;
        private float _timer;
        private Light _light;
    }
}