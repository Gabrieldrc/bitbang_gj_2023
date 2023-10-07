using UnityEngine;


public class EnemyBouncer : Enemy
{
    public Vector3 direction;
    [SerializeField] private float _speed;
    private Range _screenWidth, _screenHeight;

    void Start()
    {
        direction = Random.insideUnitSphere;
        _screenHeight = GameManager.instance.screenHeigh;
        _screenWidth = GameManager.instance.screenWidth;
    }

    void Update()
    {
        if (!IsAlive) return;

        Vector3 newPosition = transform.position + direction * (_speed * Time.deltaTime);
        if (newPosition.x < _screenWidth.min || newPosition.x > _screenWidth.max)
        {
            direction.x = -direction.x;
        }

        if (newPosition.y < _screenHeight.min || newPosition.y > _screenHeight.max)
        {
            direction.y = -direction.y;
        }

        if (newPosition.x <= transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position = newPosition;
    }
}