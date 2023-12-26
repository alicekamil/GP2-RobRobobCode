using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntEventChannel", menuName = "Events/Int Event Channel")]
public class IntEventChannel : ScriptableObject
{
    public event UnityAction<int> EventRaised;
    public void RaiseEvent(int value) => EventRaised?.Invoke(value);
}
