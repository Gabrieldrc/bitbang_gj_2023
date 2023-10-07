using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RainDrop : MonoBehaviour
{
	[SerializeField, Range(0,1f)] protected float _speed;
	[SerializeField] protected float _live;
	[SerializeField] private FloatValueSO _liveSO;
	[SerializeField] protected Rigidbody2D _rb2d;
	protected void Move()
	{
		_rb2d.gravityScale = _speed;
	}
	protected void OnEnable()
	{
		Move();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		_liveSO.value += _live;
		_liveSO.Notify();
		Destroy(gameObject);
	}
}
