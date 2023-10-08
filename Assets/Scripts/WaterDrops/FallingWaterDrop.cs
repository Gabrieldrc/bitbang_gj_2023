using System.Collections.Generic;
using UnityEngine;

public class FallingWaterDrop : MonoBehaviour
{
	[SerializeField] private List<GameObject > _waterDrops;
	[SerializeField] private float _minCooldown, _maxCooldown;

	private float _cooldown;
	private float _timer;
	private Range _screenWidth;

	private void Start()
	{
		_screenWidth = GameManager.instance.screenWidth;
		InicializeCooldown();
		_timer = _cooldown;
	}

	private void InicializeCooldown()
	{
		_cooldown = Random.Range(_minCooldown, _maxCooldown);
	}

	private void Update()
	{
		_timer -= Time.deltaTime;

		if (_timer <= 0)
		{
			_timer = _cooldown;
			SpawnWaterDrop();
		}
	}

	private void SpawnWaterDrop()
	{
		Vector3 dropPos = new Vector3(Random.Range(_screenWidth.min, _screenWidth.max), transform.position.y, transform.position.z);
		Instantiate(_waterDrops[Random.Range(0, _waterDrops.Count)], dropPos, transform.rotation);
		InicializeCooldown();
	}
}
