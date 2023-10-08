using UnityEngine;

public class GameOverUIController : MonoBehaviour
{
    [SerializeField] private FloatValueSO _liveSO;
    [SerializeField] private GameObject _gameOverScreen;

    private void OnEnable()
    {
        _liveSO.OnValueModified += GameOverHandler;
    }
    
    private void OnDisable()
    {
        _liveSO.OnValueModified -= GameOverHandler;
    }

    private void GameOverHandler(float live)
    {
        if (live > 0f) return;
        _gameOverScreen.SetActive(true);
    }
}
