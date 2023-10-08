using System;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.AddLive(-Int32.MaxValue);
            return;
        }

        Destroy(collision.gameObject);
    }
}