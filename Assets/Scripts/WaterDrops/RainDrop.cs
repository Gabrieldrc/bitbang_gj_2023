using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RainDrop : MonoBehaviour
{
	[SerializeField, Range(0,1f)] protected float _speed;
	[SerializeField] protected float _score;
	[SerializeField] protected Rigidbody2D _rb2d;
	protected void Move()
	{
		_rb2d.gravityScale = _speed;
	}
	protected void OnEnable()
	{
		Move();
	}
}
