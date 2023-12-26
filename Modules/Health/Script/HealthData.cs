using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHealth", menuName = "Data/HealthData")]
public class HealthData : ScriptableObject
{
    public event Action<int> Updated;
    public event Action Died;

    public int MaxHealth => _maxHealth;

    [SerializeField]
    private int _maxHealth;
    [NonSerialized]
    private int _currentHealth;

    public void Setup()
    {
        _currentHealth = _maxHealth;
    }

    public void DealDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log($"We got {damage}, hp left {_currentHealth}");
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Died?.Invoke();
            Debug.Log("Oh no we died!");
        }
        Updated?.Invoke(_currentHealth);
    }
}