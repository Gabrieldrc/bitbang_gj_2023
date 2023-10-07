using UnityEngine;

public class Destroyer : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) return;
		Destroy(collision.gameObject);
	}
}