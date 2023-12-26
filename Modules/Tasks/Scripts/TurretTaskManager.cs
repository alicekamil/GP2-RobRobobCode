using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace SpaceGame
{
    public class TurretTaskManager : MonoBehaviour
    {
        public bool CheckDeliverTaskItem(string itemId)
        {
            bool canDeliver = _currentTask != null && _currentTask.Data.ItemId == itemId;
            
            // _interactableOutline.OutlineColor = canDeliver ? _goodColor : _badColor;
            
            return canDeliver;
        }

        public void DeliverTaskItem()
        {
            OnTaskCompleted();
            _particles.PlayParticle();
        }

        public void OnRailgunCompleted()
        {
            if (_currentTask != null)
            {
                OnTaskCompleted();
                _particles.StopParticle();
            }
        }
        
        private void OnTaskCompleted()
        {
            _taskCompleteEvent.RaiseEvent();
            GameManager.Instance.AddScore(_currentTask.Data.ScoreReward, transform.position + Vector3.up * 1.5f);
            StartTaskCooldown(false);
            _arrowCanvas.enabled = false;
        }

        private void OnTaskFailed()
        {
            for (int i = 0; i < _currentTask.Data.WallCount; i++)
                RepairManager.Instance.BreakRandom();
            for (int i = 0; i < _currentTask.Data.FireCount; i++)
                FireSpawnManager.Instance.SpawnFire();
            
            if(_currentTask.Data.RailGunReduce)
                RailgunManager.Instance.ReduceRailgunTimer();
            if (_currentTask.Data.BatteryReduce)
                RobotInteractable._instance.ReduceBattery();
            
            _taskFailEvent.RaiseEvent();
            StartTaskCooldown(true);
            _particles.StopParticle();
        }

        private void StartTaskCooldown(bool failed)
        {
            _particles.PlayParticle();
            float multiplier = EncounterManager.CurrentEncounter.CooldownMultiplier;
            _taskCooldownTimer = failed ? 1f : _currentTask.Data.Cooldown * multiplier;
            _taskCooldownDuration = failed ? 1f : _currentTask.Data.Cooldown * multiplier;
            _taskIcon.sprite = _reloadIcon;
            _recipeHint.SetRecipe(null, _recipeIndex);
            _iconParent.SetActive(false);
            
            _interactableOutline.enabled = false;

            _currentTask = null;
        }
        
        public void GetNewTask()
        {
            //Seth edit
            //var taskData = _availableTasks[Random.Range(0, _availableTasks.Count)];
            var availableTasks = EncounterManager.CurrentEncounter.AvailableTasks;
            var taskData = availableTasks[Random.Range(0, availableTasks.Count)];
            bool isRareTask = ItemDatabase.Get(taskData.ItemId).IsSpecialAmmo;
            if (isRareTask)
            {
                _recipeHint.SetRecipe(taskData, _recipeIndex);
            }

            //_iconParent.SetActive(true);
            _iconImage.sprite = taskData.ResultIcon;
            _taskIcon.sprite = taskData.ResultIcon;
            
            _interactableOutline.enabled = true;
            // _interactableOutline.OutlineColor = _badColor;
            
            _currentTask = new Task(taskData);
        }
        
        private void Start()
        {
            GetNewTask();
            _interactableOutline.OutlineColor = _badColor;
            _arrowCanvas.enabled = false;
        }

        private void Update()
        {
            if (GameManager.Instance.IsGameplayPaused)
                return;
            if (_currentTask != null)
            {
                _currentTask.Update();
                float progress = _currentTask.GetProgress();
                // Update progress bar
                _taskProgressBar.Progress = progress;
                _taskProgressBar.Color = _taskWaitGradient.Evaluate(0f);
                
                _interactableOutline.OutlineColor = !_currentTask.IsFailed && GameManager.Instance.AnyPlayerHoldingItem(_currentTask.Data.ItemId)
                    ? _goodColor
                    : _badColor;
                
                _arrowCanvas.enabled = !_currentTask.IsFailed && GameManager.Instance.AnyPlayerHoldingItem(_currentTask.Data.ItemId)
                    ? _arrowCanvas.enabled = true
                    : _arrowCanvas.enabled = false;

                if (_currentTask.IsFailed)
                {
                    OnTaskFailed();
                }
            }
            else if (_taskCooldownTimer > 0f)
            {
                _taskCooldownTimer -= Time.deltaTime;
                _taskProgressBar.Progress = 1f - _taskCooldownTimer / _taskCooldownDuration;
                _taskProgressBar.Color = _taskCooldownGradient.Evaluate(0f);
                if (_taskCooldownTimer <= 0f)
                {
                    GetNewTask();
                    _particles.StopParticle();
                }
            }

            float speed = 3f;
            float time = Mathf.PingPong(Time.time * speed, 1);
            _interactableOutline.OutlineWidth = Mathf.Lerp(2, 3, time);
        }

        [SerializeField]
        private List<TaskData> _availableTasks;
        [SerializeField]
        private ProgressBar _taskProgressBar;
        [SerializeField]
        private Image _taskIcon;
        [SerializeField]
        private GameObject _taskIconParent;
        [SerializeField]
        private Gradient _taskWaitGradient;
        [SerializeField]
        private Gradient _taskCooldownGradient;
        [SerializeField]
        private Sprite _reloadIcon;
        [SerializeField]
        private RecipeHint _recipeHint;
        [SerializeField]
        private int _recipeIndex;
        [SerializeField]
        private VoidEventChannel _taskCompleteEvent;
        [SerializeField]
        private VoidEventChannel _taskFailEvent;
        [SerializeField] private Image _iconImage;
        [SerializeField] private GameObject _iconParent;
        [SerializeField] private LaserShoot _particles;
        [Header("Outline")]
        [SerializeField]
        private Color _goodColor;
        [SerializeField]
        private Color _badColor;
        [SerializeField]
        private Outline _interactableOutline;
        [SerializeField]
        private Canvas _arrowCanvas;

        private Task _currentTask;
        private float _taskCooldownTimer;
        private float _taskCooldownDuration;
        [SerializeField]
        private float _chance = 0.5f;
    }
}