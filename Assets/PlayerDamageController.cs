using System;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    public event Action<float> OnTakeDamage;
    public bool active = true;
    public void TakeDamage(float damage)
    {
        if (!active) return;
        OnTakeDamage?.Invoke(damage);
    }
}