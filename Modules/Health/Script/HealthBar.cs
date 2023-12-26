using UnityEngine;

namespace SpaceGame
{
    [RequireComponent(typeof(ProgressBar))]
    public class HealthBar : MonoBehaviour
    {
        private void Start()
        {
            _progressBar = GetComponent<ProgressBar>();
            _health.Updated += UpdateProgressBar;
            _health.Died += HideProgressBar;
            // TODO: Update to current health instead (by calling an event?)
            UpdateProgressBar(_health.MaxHealth);
        }

        private void HideProgressBar()
        {
            _progressBar.gameObject.SetActive(false);
        }

        private void UpdateProgressBar(int health)
        {
            _progressBar.Progress = (float) health / _health.MaxHealth;
        }

        [SerializeField] private HealthData _health;
        private ProgressBar _progressBar;
    }
}