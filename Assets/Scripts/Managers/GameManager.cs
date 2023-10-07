using UnityEngine;

[DefaultExecutionOrder(-10)]
public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _liveSO.value = _maxliveSO.value;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] private Transform _playerPosRef;
    [SerializeField] private FloatValueSO _liveSO;
    [SerializeField] private FloatValueSO _maxliveSO;
    [SerializeField] protected IntValueSO _scoreSO;
    [SerializeField] private float _loseLiveScale = 2f;

    public Transform PlayerPosRef
    {
        get => _playerPosRef;
        set => _playerPosRef = value;
    }

    private void Update()
    {
        _liveSO.value -= Time.deltaTime * _loseLiveScale;
        _liveSO.Notify();
    }

    public void AddScore(int score)
    {
        Debug.Log(score);
        _scoreSO.value += score;
        _scoreSO.Notify();
    }
}