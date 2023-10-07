using UnityEngine;

public class FlyingEnemy : Enemy
{
	[SerializeField] protected float _speed;
	[SerializeField] private bool _following;
	[SerializeField] private SpriteRenderer _spriteR;

	public bool Following
	{
		get => _following;
		set => _following = value;
	}

	private void Update()
	{
		if (_following)
		{
			FollowPlayer();
		}
		else return;
	}
	private void FollowPlayer()
	{
		Vector3 playerPosition = GameManager.instance.PlayerPosRef.position;

		Vector3 direction = playerPosition - transform.position;
		transform.Translate(_speed * Time.deltaTime * direction);

		if (playerPosition.x <= transform.position.x)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
		else
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}


	}


	
}
