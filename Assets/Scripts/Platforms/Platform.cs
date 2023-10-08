using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _moveDirection;
    [SerializeField] private Transform _positionToObjectoOnTop;
    private PlatformEffector2D _platformEffector2D;

    private void Awake()
    {
        _platformEffector2D = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        Vector3 moveDir = new Vector3(_moveDirection.x, _moveDirection.y, transform.position.z);
        transform.position += moveDir * _speed * Time.deltaTime;
    }

    public void SetObjectOnTop(GameObject objectOnTop)
    {
        objectOnTop.transform.SetParent(transform);
        objectOnTop.transform.position = _positionToObjectoOnTop.position;
    }

    public void Reverse()
    {
        StartCoroutine(CO_Reverse());
    }

    private IEnumerator CO_Reverse()
    {
        _platformEffector2D.rotationalOffset = 180f;
        yield return new WaitForSeconds(.5f);
        _platformEffector2D.rotationalOffset = 0f;
    }
}