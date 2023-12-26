using UnityEngine;

namespace SpaceGame
{
    [CreateAssetMenu(fileName = "Task", menuName = "Data/TaskData")]
    public class TaskData : ScriptableObject
    {
        public float TimeToDeliver => _timeToDeliver;
        public float Cooldown => _cooldown;
        public string ItemId => _itemId;
        public Sprite ResultIcon => _resultIcon;
        public Sprite RecipeIcon => _recipeIcon;
        public Sprite ModifierIcon => _modifierIcon;
        public int WallCount => _wallCount;
        public int FireCount => _fireCount;
        public int ScoreReward => _scoreReward;
        public bool RailGunReduce => _railGunReduce;
        public bool BatteryReduce => _batteryReduce;
        
        [SerializeField]
        private float _timeToDeliver;
        [SerializeField]
        private float _cooldown;
        [SerializeField]
        private string _itemId;
        [SerializeField]
        private Sprite _resultIcon;
        [SerializeField]
        private Sprite _recipeIcon;
        [SerializeField]
        private Sprite _modifierIcon;
        [SerializeField]
        private int _wallCount;
        [SerializeField]
        private int _fireCount;
        [SerializeField]
        private int _scoreReward = 1;
        [SerializeField] 
        private bool _railGunReduce;
        [SerializeField] 
        private bool _batteryReduce;
    }
}