using UnityEngine;

public class TimeScale : MonoBehaviour
{
    public static void Set(float value)
    {
        Mathf.Clamp(value, 0f, 1f);
        Time.timeScale = value;
    }
}
