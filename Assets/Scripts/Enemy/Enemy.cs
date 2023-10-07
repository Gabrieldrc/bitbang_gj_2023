using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    [SerializeField] protected int _deadLayer;
    [SerializeField] private int _score;
    [SerializeField] private float _damage;
    protected bool _isAlive = true;

    public bool IsAlive => _isAlive;

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Throw(Vector2 throwDirection)
    {
        _isAlive = false;
        gameObject.layer = _deadLayer;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.AddForce(throwDirection, ForceMode2D.Impulse);
        GameManager.instance.AddScore(_score);
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (IsAlive)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                var player = col.GetComponent<Player>();
                if (!player.IsSuperPowerful)
                {
                    player.TakeDamage(_damage);
                }
            }
        }
        else
        {
            if (col.gameObject.CompareTag("Enemy") && col.gameObject != gameObject)
            {
                var otherEnemy = col.GetComponent<Enemy>();
                if (otherEnemy.IsAlive)
                {
                    otherEnemy.Throw((otherEnemy.transform.position - transform.position).normalized * 15);
                }
            }
        }
    }
}