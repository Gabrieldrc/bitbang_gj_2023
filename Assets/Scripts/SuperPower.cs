using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SuperPower : MonoBehaviour
{
	[SerializeField] private AudioClip _soundEffect;
	private AudioSource _aSource;


	private void Start()
	{
		_aSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) return;
		UsePower();
	}

	private void UsePower()
	{
		StartCoroutine(CO_UsePowerEffect());
	}
	IEnumerator CO_UsePowerEffect()
	{
		_aSource.PlayOneShot(_soundEffect);
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;

		while (_aSource.isPlaying)
		{
			yield return null;
		}
		Destroy(gameObject);
	}
}
