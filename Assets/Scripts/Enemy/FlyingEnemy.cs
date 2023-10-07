using UnityEngine;

public class FlyingEnemy : Enemy
{
    [SerializeField] protected float _speed;

    private void FollowPlayer()
	{
		//Mirar al player (rotacion apunta al player)
		//Perseguir al player (transform quiere llegar a ser GameManager.instance.PlayerPosRef)
		LookAtPlayer();

	}
	private void LookAtPlayer()
	{
		//Vector3 playerPosToLookAt = new Vector3(0, 0, GameManager.instance.PlayerPosRef.position.x);
		//Debug.Log($"Enemy is looking at player {playerPosToLookAt}");
		//transform.LookAt(playerPosToLookAt);


	}
	private void StopFollowing()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) return;
		FollowPlayer();
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) return;
		StopFollowing();
	}
}
