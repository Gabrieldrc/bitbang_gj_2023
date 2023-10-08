using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyFollowPrefab;
    [SerializeField] private GameObject _enemyNormalPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _maxTimeToSpawn = 4f;
    [SerializeField, Range(0f, 1f)] private float _followChance = .4f;
    private float _timeToSpawn = 0f;

    private void Awake()
    {
        _timeToSpawn = _maxTimeToSpawn;
    }

    private void Update()
    {
        if (_timeToSpawn < 0)
        {
            GameObject prefab;
            var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            prefab = Random.Range(0f, 1f) > _followChance ? _enemyFollowPrefab : _enemyNormalPrefab;
            Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            _timeToSpawn = _maxTimeToSpawn;
        }
        else
        {
            _timeToSpawn -= Time.deltaTime;
        }
    }
}