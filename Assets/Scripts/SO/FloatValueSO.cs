using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatValueSO", menuName = "ScriptableObjects/FloatValueSO", order = 1)]
public class FloatValueSO : ScriptableObject
{
    public event Action<float> OnValueModified;

    [SerializeField] private float _value = 0;

    public float value
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
