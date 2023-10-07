using UnityEngine;

public class Destroyer : MonoBehaviour
{
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) return;
		Destroy(collision.gameObject);
	}
}