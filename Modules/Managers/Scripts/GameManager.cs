using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceGame
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public bool IsGameplayPaused => _isGameplayPaused;
        public int Score => _score;
        public int Highscore => _highscore;

        public bool AnyPlayerHoldingItem(string id) => _players.Any(p => p.HeldItemId == id);

        public void GoMenu()
        {
            LeanTween.cancelAll();
            SceneManager.LoadScene("Intro");
        }
        
        protected override void Awake()
        {
            LeanTween.init( 800 );
            // TODO: Make sure we properly dispose of events later
            _allyHealth.Setup();
            _enemyHealth.Setup();
            _scoreEvent.RaiseEvent(_score);
            _players = FindObjectsOfType<CharacterLogic>();
            
            base.Awake();
            Time.timeScale = 1;
            StartCoroutine(CoStartGame());

            _highscore = PlayerPrefs.GetInt("highscore", 0);
        }
        
        private void OnEnable()
        {
            _taskCompleteEvent.EventRaised += OnTaskCompleted;
            _taskFailEvent.EventRaised += OnTaskFailed;
            _railgunFireEvent.EventRaised += OnRailgunFired;
            _outOfOxygenEvent.EventRaised += OnOutOfOxygen;
            _allyHealth.Died += GameOver;
        }

        private void OnDisable()
        {
            _taskCompleteEvent.EventRaised -= OnTaskCompleted;
            _taskFailEvent.EventRaised -= OnTaskFailed;
            _railgunFireEvent.EventRaised -= OnRailgunFired;
            _outOfOxygenEvent.EventRaised -= OnOutOfOxygen;
            _allyHealth.Died -= GameOver;
        }

        private IEnumerator CoStartGame()
        {
            ToggleGameplayPause(true);
            if (!_skipControlsHint)
            {
                // Let players read the controls
                _gameUI.ShowControls();
                yield return new WaitForSecondsRealtime(5f);
                _gameUI.FadeControls();
            }
            yield return new WaitForSecondsRealtime(3f);
            ToggleGameplayPause(false);
        }

        private void Update()
        {
            // if (Input.GetMouseButtonDown(0))
            //     RepairManager.Instance.BreakRandom();
        }

        public void ToggleGameplayPause(bool paused) => _isGameplayPaused = paused;

        public void Restart()
        {
            LeanTween.cancelAll();
            Time.timeScale = 1;
            SceneManager.LoadScene("Game");
        }

        [ContextMenu("Game Over!")]
        public void GameOver()
        {
            _gameUI.ShowGameOver();
            Time.timeScale = 0f;
        }

        public void AddScore(int value, Vector3 position)
        {
            int score = value * EncounterManager.CurrentEncounter.ScoreMultiplier;
            _score += score;
            ParticleManager.Instance.SpawnScorePopup(score, position);
            _scoreEvent.RaiseEvent(_score);

            if (_score > _highscore)
            {
                _highscore = _score;
                PlayerPrefs.SetInt("highscore", _highscore);
            }
        }

        private void OnTaskCompleted()
        {
            //
        }
        
        private void OnTaskFailed()
        {
            // Deal damage to the player's ship
            // _allyHealth.DealDamage(1);
            CameraControl.Instance.Shake();
        }
        
        private void OnRailgunFired()
        {
            // _enemyHealth.DealDamage(1);
        }

        private void OnOutOfOxygen()
        {
            GameOver();
        }

        private int _score;
        private int _highscore;
        private bool _isGameplayPaused;
        private CharacterLogic[] _players;

        [SerializeField] private GameUI _gameUI;
        [SerializeField] private bool _skipControlsHint;
        [Header("Health")]
        [SerializeField] private HealthData _allyHealth;
        [SerializeField] private HealthData _enemyHealth;
        [Header("Events")]
        [SerializeField] private VoidEventChannel _taskCompleteEvent;
        [SerializeField] private VoidEventChannel _taskFailEvent;
        [SerializeField] private VoidEventChannel _railgunFireEvent;
        [SerializeField] private VoidEventChannel _outOfOxygenEvent;
        [SerializeField] private IntEventChannel _scoreEvent;
    }
}