using System.Collections;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
	[SerializeField] private Sprite _trashCanTransform, _normal;
	[SerializeField] private float _tranformationTime;
	[SerializeField] private SpriteRenderer _currentSprite;
	[SerializeField] private AudioClip _gritoSapucai;
	private AudioSource _aSource;
	private void Start()
	{
		_currentSprite.sprite = _normal;
		_aSource = GetComponentInParent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PowerTrash"))
		{
			StartCoroutine(CO_UsePower());
		}
	}

	private void ActivatePower()
	{
		Debug.Log("Trash man llama");
		_currentSprite.sprite = _trashCanTransform;
		_aSource.PlayOneShot(_gritoSapucai);
	}
	private void DeactivatePower()
	{
		_currentSprite.sprite = _normal;
		Debug.Log("Normal man llama");
	}
	

	private IEnumerator CO_UsePower()
	{
		ActivatePower();
		yield return new WaitForSeconds(_tranformationTime);
		DeactivatePower();
	}
}
