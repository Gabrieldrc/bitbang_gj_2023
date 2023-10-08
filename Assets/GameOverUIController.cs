using UnityEngine;

public class GameOverUIController : MonoBehaviour
{
    [SerializeField] private FloatValueSO _liveSO;
    [SerializeField] private Canvas _gameOverCanvas;

    private void OnEnable()
    {
        _liveSO.OnValueModified += GameOverHandler;
    }

    private void GameOverHandler(float live)
    {
        if (live > 0f) return;
        _gameOverCanvas.enabled = true;
    }
}
