using UnityEngine;

[DefaultExecutionOrder(-10)]
public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;
    public Range screenHeigh;
    public Range screenWidth;

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

        var mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No camera");
            return;
        }

        var screenPosition = (Vector2)mainCamera.transform.position;
        var height = 2f * mainCamera.orthographicSize;
        var width = height * mainCamera.aspect;
        screenHeigh = new Range(screenPosition.y - (height / 2), screenPosition.y + (height / 2));
        screenWidth = new Range(screenPosition.x - (width / 2), screenPosition.x + (width / 2));
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

    private void Start()
    {
        _scoreSO.value = 0;
        _scoreSO.Notify();
    }

    private void Update()
    {
        _liveSO.value -= Time.deltaTime * _loseLiveScale;
        _liveSO.Notify();
    }

    public void AddScore(int score)
    {
        _scoreSO.value += score;
        _scoreSO.Notify();
    }
}