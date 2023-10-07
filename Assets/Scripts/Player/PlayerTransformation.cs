using System.Collections;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
	[SerializeField] private Sprite _trashCanTransform, _normal;
	[SerializeField] private float _tranformationTime;
	[SerializeField] private SpriteRenderer _currentSprite;

	private void Start()
	{
		_currentSprite.sprite = _normal;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PowerTrash"))
		{
			ActivatePower();
		}
	}

	private void ActivatePower()
	{
		StartCoroutine(CO_UsePower());
	}



	private IEnumerator CO_UsePower()
	{
		Debug.Log("Trash man llama");
		_currentSprite.sprite = _trashCanTransform; 
		yield return new WaitForSeconds(_tranformationTime);
		_currentSprite.sprite = _normal;
		Debug.Log("Normal man llama");
	}
}
