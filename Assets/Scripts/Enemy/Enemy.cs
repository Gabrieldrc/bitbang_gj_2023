using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private int _deadLayer;

    private void Awake()
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
