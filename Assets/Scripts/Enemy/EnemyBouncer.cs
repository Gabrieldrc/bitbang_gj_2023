using UnityEngine;


public class EnemyBouncer : Enemy
{
	public Vector3 direction;
	[SerializeField] private float _speed;
	private Range _screenWidth, _screenHeight;

	void Start()
	{
		direction = Random.insideUnitSphere;
		_screenHeight = GameManager.instance.screenHeigh;
		_screenWidth = GameManager.instance.screenWidth;
	}

	void Update()
	{
		Vector3 newPosition = new Vector3(transform.position.x + direction.x * _speed * Time.deltaTime, transform.position.y + direction.y * _speed * Time.deltaTime, transform.position.z);
		if (newPosition.x < _screenWidth.min || newPosition.x > _screenWidth.max)
		{
			direction.x = -direction.x;
		}

		if (newPosition.y < _screenHeight.min || newPosition.y > _screenHeight.max)
		{
			direction.y = -direction.y;
		}

		transform.position = newPosition;
	}
}