using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    [SerializeField] protected int _deadLayer;
    [SerializeField] private int _score;
    [SerializeField] private float _damage;

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Throw(Vector2 throwDirection)
    {
        gameObject.layer = _deadLayer;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.AddForce(throwDirection, ForceMode2D.Impulse);
        GameManager.instance.AddScore(_score);
    }

    protected void OnTriggerEnter2D(Collider2D col)
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
}