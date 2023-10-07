using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IntValueSO", menuName = "ScriptableObjects/IntValueSO", order = 1)]
public class IntValueSO : ScriptableObject
{
    public event Action<int> OnValueModified;

    [SerializeField] private int _value = 0;

    public int value
    {
        get => _value;
        set
        {
            _value = value;
        }
    }

    public void Notify()
    {
        OnValueModified?.Invoke(_value);
    }
}
