using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _progressText; // Добавляем текст для отображения прогресса
    
    private ReactiveVariable<float> _current;
    private ReactiveVariable<float> _max;

    public void Initialize(ReactiveVariable<float> current, ReactiveVariable<float> max)
    {
        this._current = current;
        this._max = max;

        if (_current != null)
            _current.Changed += OnCurrentChanged;
        if (_max != null)
            _max.Changed += OnMaxChanged;

        UpdateValue(_current.Value, _max.Value);

        Debug.Log($"[SliderView] Инициализация: {_current.Value}/{_max.Value}");
    }

    private void OnDestroy()
    {
        if (_current != null)
            _current.Changed -= OnCurrentChanged;
        if (_max != null)
            _max.Changed -= OnMaxChanged;
    }

    private void OnMaxChanged(float oldValue, float newValue)
    {
        Debug.Log($"[SliderView] Max изменился с {oldValue} на {newValue}");
        UpdateValue(_current.Value, newValue);
    }

    private void OnCurrentChanged(float oldValue, float newValue)
    {
        Debug.Log($"[SliderView] Current изменился с {oldValue} на {newValue}");
        UpdateValue(newValue, _max.Value);
    }

    private void UpdateValue(float currentValue, float maxValue)
    {
        if (_slider == null)
        {
            Debug.LogError("[SliderView] Slider не назначен!");
            return;
        }

        if (maxValue <= 0)
        {
            _slider.value = 0;
            if (_progressText != null)
                _progressText.text = "0/0 (0%)";
            return;
        }

        float progress = currentValue / maxValue;
        _slider.value = progress;
        
        // Обновляем текст прогресса
        if (_progressText != null)
        {
            float percent = progress * 100f;
            _progressText.text = $"Собрано: {currentValue:F0}/{maxValue:F0} ({percent:F0}%)";
        }

        Debug.Log($"[SliderView] Обновление слайдера: {currentValue}/{maxValue} = {progress:F2}");
    }
}
