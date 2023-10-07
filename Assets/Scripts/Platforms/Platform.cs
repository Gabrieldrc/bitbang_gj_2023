using UnityEngine;

public class Platform : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private Vector2 _moveDirection;

	private void Update()
	{
		MovePlatform();
	}
	private void MovePlatform()
	{
		Vector3 moveDir = new Vector3(_moveDirection.x, _moveDirection.y, transform.position.z);
		transform.position += moveDir * _speed * Time.deltaTime;
	}
}