using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    [CreateAssetMenu(fileName = "Encounter", menuName = "Data/EncounterData")]
    public class EncounterData : ScriptableObject
    {
        // Cooldown multipliers, tasks, unlocked interactables, hazards
        public float NormalTaskMultiplier => _normalTaskMultiplier;
        public float SpecialTaskMultiplier => _specialTaskMultiplier;
        public float CooldownMultiplier => _cooldownMultiplier;
        public IReadOnlyList<TaskData> AvailableTasks => _availableTasks;
        public bool AllowSpecialAmmo => _allowSpecialAmmo;
        public bool AllowFires => _allowFires;
        public int EnemyHp => _enemyHp;
        public int ScoreMultiplier => _scoreMultiplier;

        [SerializeField]
        private int _scoreMultiplier = 1;
        [SerializeField]
        private int _enemyHp = 2;
        [SerializeField]
        private float _normalTaskMultiplier = 1;
        [SerializeField]
        private float _specialTaskMultiplier = 1;
        [SerializeField]
        private float _cooldownMultiplier = 1;
        [SerializeField]
        private List<TaskData> _availableTasks;
        [SerializeField]
        private bool _allowSpecialAmmo = true;
        [SerializeField]
        private bool _allowFires = true;
    }
}