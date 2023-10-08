using System.Collections.Generic;
using System.Collections;

using UnityEngine;

public class TrashcanBonus : MonoBehaviour
{
	[SerializeField] private int _score = 500;
	[SerializeField] private GameObject _particles;
	[SerializeField] private AudioClip[] _congratsAudios;
	private AudioSource _aSource;

	private void Start()
	{
		_aSource = GetComponent<AudioSource>();
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Enemy"))
		{
			var otherEnemy = col.GetComponent<Enemy>();
			if (!otherEnemy.IsAlive)
			{
				GameManager.instance.AddScore(_score);
				Destroy(otherEnemy.gameObject);
				_particles.SetActive(true);
				_aSource.PlayOneShot(_congratsAudios[Random.Range(0, _congratsAudios.Length)]);
			}
		}
	}
	//private IEnumerator CO_TrashCanSound()
	//{
	//	while (_aSource.isPlaying)
	//	{
	//		yield return null;
	//	}

	//}
}