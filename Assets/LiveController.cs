using UnityEngine;
using UnityEngine.UI;

public class LiveController : MonoBehaviour
{
    [SerializeField] private Slider _liveSlider;
    [SerializeField] private FloatValueSO _liveSO;
    [SerializeField] private FloatValueSO _maxliveSO;

    private void Start()
    {
        _liveSlider.minValue = 0;
        _liveSlider.maxValue = _maxliveSO.value;
        _liveSlider.value = _maxliveSO.value;
    }

    private void OnEnable()
    {
        _liveSO.OnValueModified += UpdateLiveUI;
    }

    private void UpdateLiveUI(float newLive)
    {
        _liveSlider.value = newLive;
    }
}
