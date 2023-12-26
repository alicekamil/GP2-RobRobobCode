using UnityEngine;
using UnityEngine.Events;

    [CreateAssetMenu(fileName = "VoidEventChannel", menuName = "Events/Void Event Channel")]
    public class VoidEventChannel : ScriptableObject
    {
        public event UnityAction EventRaised;
        public void RaiseEvent() => EventRaised?.Invoke();
    }
