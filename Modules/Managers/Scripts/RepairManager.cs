using System.Linq;
using UnityEngine;

namespace SpaceGame
{
    public class RepairManager : MonoSingleton<RepairManager>
    {
        public int TotalDamaged => _totalDamaged;
        
        public void BreakRandom()
        {
            var available = _repairInteractables.Where(r => r.IsRepaired).ToArray();
            if (available.Length == 0)
            {
                Debug.LogWarning("All interactables are broken!");
                return;
            }
            
            available[Random.Range(0, available.Length)].Break();
        }
        
        protected override void Awake()
        {
            base.Awake();
            _repairInteractables = FindObjectsOfType<RepairInteractable>();

            foreach (var interactable in _repairInteractables)
            {
                interactable.Damaged += OnDamaged;
                interactable.Repaired += OnRepaired;
            }
        }

        private void OnDamaged() => _totalDamaged++;

        private void OnRepaired() => _totalDamaged--;

        private int _totalDamaged;
        private RepairInteractable[] _repairInteractables;
    }
}