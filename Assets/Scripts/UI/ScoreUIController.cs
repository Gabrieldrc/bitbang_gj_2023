using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTextValue;
    [SerializeField] private IntValueSO _scoreSO;

    private void Start()
    {
        _scoreTextValue.text = _scoreSO.value.ToString();
    }

    private void OnEnable()
    {
        _scoreSO.OnValueModified += UpdateScoreUI;
    }

    private void UpdateScoreUI(int newScore)
    {
        _scoreTextValue.text = newScore.ToString();
    }
}
