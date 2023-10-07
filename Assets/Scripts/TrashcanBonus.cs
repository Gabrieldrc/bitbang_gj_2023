using UnityEngine;

public class TrashcanBonus : MonoBehaviour
{
    [SerializeField] private int _score = 500;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            var otherEnemy = col.GetComponent<Enemy>();
            if (!otherEnemy.IsAlive)
            {
                GameManager.instance.AddScore(_score);
                Destroy(otherEnemy.gameObject);
            }
        }
    }
}