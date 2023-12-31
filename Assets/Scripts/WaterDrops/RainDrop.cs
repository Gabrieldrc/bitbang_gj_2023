using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RainDrop : MonoBehaviour
{
	[SerializeField, Range(0,1f)] protected float _speed;
	[SerializeField] protected float _live;
	[SerializeField] protected Rigidbody2D _rb2d;
	protected AudioSource _aSource;
	protected void Move()
	{
		_rb2d.gravityScale = _speed;
	}
	protected void OnEnable()
	{
		Move();
		_aSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			StartCoroutine(CO_DropEffect());
			
		}
	}

	private IEnumerator CO_DropEffect()
	{
		GameManager.instance.AddLive(_live);
		_aSource.Play();
		GetComponent<CapsuleCollider2D>().enabled = false;
		GetComponent<SpriteRenderer>().enabled = false;
		while (_aSource.isPlaying)
		{
			yield return null;
		}
		Destroy(gameObject);
	}

}
