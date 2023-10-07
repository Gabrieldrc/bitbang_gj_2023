using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    [SerializeField] protected int _deadLayer;

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Throw(Vector2 throwDirection)
    {
        gameObject.layer = _deadLayer;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.AddForce(throwDirection, ForceMode2D.Impulse);
    }
}
