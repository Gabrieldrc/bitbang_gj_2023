using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event Action OnEnemyDetected;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;
        if (col.transform.position.y > transform.position.y) return;
        OnEnemyDetected?.Invoke();
    }
}
