using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
	[SerializeField] private FlyingEnemy _enemyRef;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) return;
		_enemyRef.Following = true;
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) return;
		_enemyRef.Following = false;
	}
}
