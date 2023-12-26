using UnityEngine;

namespace SpaceGame
{
    public class AntennaLight : MonoBehaviour
    {
        private void Awake()
        {
            _robotInteractable = GetComponent<RobotInteractable>();
        }

        private void Update()
        {
            _level = _robotInteractable._batteryLevel / 100f;
            _particle.startColor = _gradient.Evaluate(_level);
            _light.color = _gradient.Evaluate(_level);

            if (_level == 0)
            {
                Flashing();
            }
            else
            {
                _light.enabled = true;
            }
        }

        private void Flashing()
        {
            if (_blinkTimer > 0)
                _blinkTimer -= Time.deltaTime;

            if (_blinkTimer <= 0)
            {
                _light.enabled = !_light.enabled;
                _blinkTimer = 0.5f;
            }
        }

        [SerializeField] private Light _light;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private float _level;
        [SerializeField] private RobotInteractable _robotInteractable;
        [SerializeField] private float _blinkTimer;
        [SerializeField] private ParticleSystem _particle;
    }
}