using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoolEventChannel", menuName = "Events/Bool Event Channel")]
public class BoolEventChannel : ScriptableObject
{
    public event UnityAction<bool> EventRaised;
    public void RaiseEvent(bool value) => EventRaised?.Invoke(value);
}
