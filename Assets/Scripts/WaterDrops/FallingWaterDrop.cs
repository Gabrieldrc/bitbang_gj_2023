using System.Collections.Generic;
using UnityEngine;

public class FallingWaterDrop : MonoBehaviour
{
	[SerializeField] private float _mixX, _maxX;
	[SerializeField] private List<GameObject > _waterDrops;
	[SerializeField] private float _minCooldown, _maxCooldown;

	private float _cooldown;
	private float _timer;

	private void Start()
	{
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
		Vector3 dropPos = new Vector3(Random.Range(_mixX, _maxX), transform.position.y, transform.position.z);
		Instantiate(_waterDrops[Random.Range(0, _waterDrops.Count)], dropPos, transform.rotation);
		InicializeCooldown();
	}

}
