using UnityEngine;

public class FlyingEnemy : Enemy
{
    [SerializeField] protected float _speed;
	[SerializeField] private bool _following;
	[SerializeField] private SpriteRenderer _spriteR;

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

		if (playerPosition.x <= transform.position.x )
		{
			//transform.eulerAngles = new Vector3(0, 0, 0);
			transform.localScale = new Vector3(1, 1, 1);
		}
		else
		{
			//transform.eulerAngles = new Vector3(0, 180, 0);
			transform.localScale = new Vector3(-1, 1, 1);
		}


	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) return;
		_following = true;
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) return;
		_following = false;
	}
}
