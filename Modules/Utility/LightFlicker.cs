using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    void Start()
    {
        _timer = Random.Range(_minTime, _maxTime);
    }

    void Update()
    {
        FlickerLight();
    }

    void FlickerLight()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            _light.enabled = !_light.enabled;
            _timer = Random.Range(_minTime, _maxTime);
        }
    }

    [SerializeField] private Light _light;
    [SerializeField] private float _maxTime;
    [SerializeField] private float _minTime;
    [SerializeField] private float _timer;
}