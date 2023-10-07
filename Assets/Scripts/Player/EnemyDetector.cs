using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private Enemy _currentEnemy;
    public event Action OnEnemyDetected;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;
        if (col.transform.position.y > transform.position.y) return;
        _currentEnemy = col.GetComponent<Enemy>();
        OnEnemyDetected?.Invoke();
    }

    public void ThrowEnemy(Vector2 attackDirection)
    {
        _currentEnemy.Throw(attackDirection);
    }
}
