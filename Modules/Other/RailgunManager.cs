using System.Collections;
using UnityEngine;

namespace SpaceGame
{
    public class RailgunManager : MonoSingleton<RailgunManager>
    {
        public void Fire()
        {
            StartCoroutine(CoFire());
        }

        private void Update()
        {
            if (GameManager.Instance.IsGameplayPaused)
                return;
            if (!_readyToFire)
            {
                _timer += Time.deltaTime;
                _launchBar.Progress = 0f + _timer / _timeUntilFire;
                if (_timer >= _timeUntilFire)
                {
                    _readyToFire = true;
                    _railgunChargedEvent.RaiseEvent();
                }
            }

            _launchBar.Color = _readyToFire ? Color.green : Color.yellow;
        }

        private IEnumerator CoFire()
        {
            foreach (var player in FindObjectsOfType<CharacterLogic>())
            {
                GameManager.Instance.AddScore(_scoreReward, player.transform.position + Vector3.up * 1f);
            }

            _railgunFireEvent.RaiseEvent();
            _readyToFire = false;
            _timer = 0;
            AutoFinishTasks();

            _fullShip.SetActive(true);
            CameraControl.Instance.Camera.orthographic = false;
            CameraControl.Instance.OverrideTarget = _cameraTarget;
            CameraControl.Instance.Shake();
            _vfx.Play();
            GameManager.Instance.ToggleGameplayPause(true);
            yield return new WaitForSeconds(3.75f);
            CameraControl.Instance.Camera.orthographic = true;
            CameraControl.Instance.OverrideTarget = null;
            _fullShip.SetActive(false);
            GameManager.Instance.ToggleGameplayPause(false);
        }

        public void ReduceRailgunTimer()
        {
            _readyToFire = false;
            _timer /= 2;
        }

        public void AutoFinishTasks()
        {
            foreach (var turret in FindObjectsOfType<TurretTaskManager>())
            {
                turret.OnRailgunCompleted();
            }
        }

        public float _timer;
        public bool _readyToFire;
        [SerializeField] private float _timeUntilFire;
        [SerializeField] private ProgressBar _launchBar;
        [SerializeField] private RailgunVFX _vfx;
        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private VoidEventChannel _railgunFireEvent;
        [SerializeField] private VoidEventChannel _railgunChargedEvent;
        [SerializeField] private int _scoreReward;
        [SerializeField] private GameObject _fullShip;
    }
}