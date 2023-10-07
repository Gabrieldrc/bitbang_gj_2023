using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Platforms
{
    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] private int _divX = 3;
        [SerializeField] private int _divY = 4;
        [SerializeField] private GameObject _platformRight;
        [SerializeField] private GameObject _platformLeft;
        [SerializeField] private GameObject _platformUp;
        [SerializeField] private GameObject _platformDown;
        [SerializeField] private float _minCooldown, _maxCooldown;

        private float _cooldown;
        private float _timer;
        private Range _screenHeigh;
        private Range _screenWidth;

        private void Awake()
        {
            var mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("No camera");
                return;
            }

            var screenPosition = (Vector2)mainCamera.transform.position;
            var height = 2f * mainCamera.orthographicSize;
            var width = height * mainCamera.aspect;
            _screenHeigh = new Range(screenPosition.y - (height / 2), screenPosition.y + (height / 2));
            _screenWidth = new Range(screenPosition.x - (width / 2), screenPosition.x + (width / 2));
        }

        private void Start()
        {
            _cooldown = RandomCooldown();
            _timer = _cooldown;
        }

        private float RandomCooldown()
        {
            return Random.Range(_minCooldown, _maxCooldown);
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                _timer = RandomCooldown();
                SpawnWaterDrop();
            }
        }

        private void SpawnWaterDrop()
        {
            var platformPosition = Vector2.zero;
            GameObject platformPrefab;

            var isComingFromYAxis = Random.Range(0, 2) > 0;
            if (isComingFromYAxis)
            {
                platformPosition.y = Mathf.Lerp(_screenHeigh.min, _screenHeigh.max, (1f / (_divY + 1)) * Random.Range(1, _divY +1));
                var isComingFromLeft = Random.Range(0, 2) > 0;
                if (isComingFromLeft)
                {
                    platformPosition.x = _screenWidth.min;
                    platformPrefab = _platformRight;
                }
                else
                {
                    platformPosition.x = _screenWidth.max;
                    platformPrefab = _platformLeft;
                }
            }
            else
            {
                platformPosition.x = Mathf.Lerp(_screenWidth.min, _screenWidth.max,(1f / (_divX + 1f)) * Random.Range(1, _divX + 1));
                var isComingFromDown = Random.Range(0, 2) > 0;
                if (isComingFromDown)
                {
                    platformPosition.y = _screenHeigh.min;
                    platformPrefab = _platformUp;
                }
                else
                {
                    platformPosition.y = _screenHeigh.max;
                    platformPrefab = _platformDown;
                }
            }

            Instantiate(platformPrefab, platformPosition, Quaternion.identity);
        }
    }
}