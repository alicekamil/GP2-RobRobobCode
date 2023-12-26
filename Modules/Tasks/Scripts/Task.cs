using UnityEngine;

namespace SpaceGame
{
    public class Task 
    {
        public TaskData Data => _data;
        public bool IsFailed => _isFailed;

        // Constructor that takes a TaskData object as an argument and assigns it to _data
        public Task(TaskData data) 
        {
            _data = data;
            bool isSpecialTask = ItemDatabase.Get(data.ItemId).IsSpecialAmmo;
            float modifier = isSpecialTask
                ? EncounterManager.CurrentEncounter.SpecialTaskMultiplier
                : EncounterManager.CurrentEncounter.NormalTaskMultiplier;
            _duration = data.TimeToDeliver * modifier;
            _timer = _duration;
        }

        // Gives us a progress value from 0 (just started) to 1 (task failed). Dividing time elapsed by duration of the task.
        public float GetProgress()
        {
            return _timer / _duration;
        }

        public void Update()
        {
            _timer -= Time.deltaTime;
            
            if (_timer <= 0f)
            {
                // If the timer hits zero when we were waiting for items, then we failed the task
                _isFailed = true;
            }
        }
        
        private TaskData _data;
        private float _timer;
        private float _duration;
        private bool _isFailed; // did the timer run out
    }
}