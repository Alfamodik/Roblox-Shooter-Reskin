using UnityEngine;
using UnityEngine.UI;

public class ProgressBarsController : MonoBehaviour
{
    [SerializeField] private Slider playerProgressBar;
    [SerializeField] private Slider botProgressBar;

    // Вызывай этот метод, когда прогресс игрока меняется
    public void SetPlayerProgress(float value)
    {
        playerProgressBar.value = value;
    }

    // Вызывай этот метод, когда прогресс бота меняется
    public void SetBotProgress(float value)
    {
        botProgressBar.value = value;
    }
}