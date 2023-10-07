using UnityEngine;

public class InitialFloor : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy = 2f;
    private bool _itStarted = false;

    private void OnCollisionEnter2D(Collision2D col)
    {
        _itStarted = true;
    }

    private void Update()
    {
        if (!_itStarted) return;
        
        _timeToDestroy -= Time.deltaTime;
        if (_timeToDestroy <= 0)
            Destroy(gameObject);
    }
}