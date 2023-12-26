using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class EncounterManager : MonoSingleton<EncounterManager>
    {
        public event Action EncounterChanged;
        public static EncounterData CurrentEncounter => Instance._encounters[Instance._encounterIndex];

        protected override void Awake()
        {
            base.Awake();
        }

        private IEnumerator Start()
        {
            yield return null;
            int skipFirst = PlayerPrefs.GetInt("first", 0);
            if (skipFirst != 0)
            {
                _encounterIndex = 1;
            }
            else
            {
                PlayerPrefs.SetInt("first", 1);
            }
            
            UpdateEncounter();
        }
        
        private void OnEnable()
        {
            _railgunFireEvent.EventRaised += OnRailgunFired;
        }

        private void OnDisable()
        {
            _railgunFireEvent.EventRaised -= OnRailgunFired;
        }
        
        // Shooting railgun should damage the ship
        private void OnRailgunFired()
        {
            _enemyHp--;
            _target = (float)_enemyHp / CurrentEncounter.EnemyHp;
            if (_enemyHp <= 0)
            {
                _encounterIndex = Mathf.Clamp(_encounterIndex + 1, 0, _encounters.Length - 1);
                CameraControl.Instance.Shake();
                GameManager.Instance.AddScore(10, Vector3.zero);
                UpdateEncounter();
            }
        }

        private void UpdateEncounter()
        {
            _enemyHp = CurrentEncounter.EnemyHp;
            _enemyHealthBar.Progress = 1;
            _target = 1f;
            // Notify others
            EncounterChanged?.Invoke();
        }

        private void Update()
        {
            _enemyHealthBar.Progress =
                Mathf.MoveTowards(_enemyHealthBar.Progress, _target, _reduceSpeed * Time.deltaTime);
        }

        // Manage enemy hp, track current encounter
        [SerializeField]
        private EncounterData[] _encounters; // sequence of all encounters in the game
        [SerializeField]
        private ProgressBar _enemyHealthBar;
        private int _encounterIndex; // index of current encounter?
        private int _enemyHp;
        private float _target = 1f;
        [SerializeField] private float _reduceSpeed = 0.5f;
        [Header("Events")]
        [SerializeField] private VoidEventChannel _railgunFireEvent;
    }
}
