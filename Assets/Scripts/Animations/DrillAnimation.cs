using UnityEngine;

public class DrillAnimation : MonoBehaviour, IPausable
{
    [Header("Настройки вращения")]
    [Tooltip("Скорость вращения бура (градусы в секунду)")]
    public float rotationSpeed = 360f;

    [Tooltip("Вращать ли бур автоматически при старте")]
    public bool rotateOnStart = true;

    [Header("Настройки вибрации")]
    [Tooltip("Включить ли вибрацию бура")]
    public bool enableVibration = true;

    [Tooltip("Сила вибрации")]
    public float vibrationStrength = 0.1f;

    [Tooltip("Скорость вибрации")]
    public float vibrationSpeed = 10f;

    [Header("Настройки движения")]
    [Tooltip("Двигаться ли вперед-назад во время бурения")]
    public bool enableMovement = true;

    [Tooltip("Расстояние движения вперед-назад")]
    public float movementDistance = 0.5f;

    [Tooltip("Скорость движения вперед-назад")]
    public float movementSpeed = 2f;

    // Приватные переменные
    private bool isRotating = false;
    private Vector3 initialPosition;
    private float vibrationTimer = 0f;
    private float movementTimer = 0f;

    public bool OnPause { get; private set; }

    void Start()
    {
        // Сохраняем начальную позицию
        initialPosition = transform.position;

        // Запускаем вращение если включено
        if (rotateOnStart)
        {
            StartDrilling();
        }
    }

    void Update()
    {
        if (OnPause)
            return;

        if (isRotating)
        {
            // Вращение вокруг оси Y
            RotateDrill();

            // Вибрация если включена
            if (enableVibration)
            {
                ApplyVibration();
            }

            // Движение вперед-назад если включено
            if (enableMovement)
            {
                ApplyMovement();
            }
        }
    }

    /// <summary>
    /// Основное вращение бура
    /// </summary>
    private void RotateDrill()
    {
        float rotation = rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0, Space.World);
    }

    /// <summary>
    /// Применение вибрации к буру
    /// </summary>
    private void ApplyVibration()
    {
        vibrationTimer += Time.deltaTime * vibrationSpeed;

        // Случайное смещение для эффекта вибрации
        float randomX = Mathf.PerlinNoise(vibrationTimer, 0) * 2f - 1f;
        float randomY = Mathf.PerlinNoise(0, vibrationTimer) * 2f - 1f;
        float randomZ = Mathf.PerlinNoise(vibrationTimer, vibrationTimer) * 2f - 1f;

        Vector3 vibrationOffset = new Vector3(randomX, randomY, randomZ) * vibrationStrength;
        transform.localPosition = initialPosition + vibrationOffset;
    }

    /// <summary>
    /// Движение вперед-назад
    /// </summary>
    private void ApplyMovement()
    {
        movementTimer += Time.deltaTime * movementSpeed;

        // Синусоидальное движение для эффекта бурения
        float movementOffset = Mathf.Sin(movementTimer) * movementDistance;
        Vector3 movement = transform.forward * movementOffset;

        transform.position = initialPosition + movement;
    }

    /// <summary>
    /// Начать бурение
    /// </summary>
    public void StartDrilling()
    {
        isRotating = true;
        Debug.Log("Бурение начато");
    }

    /// <summary>
    /// Остановить бурение
    /// </summary>
    public void StopDrilling()
    {
        isRotating = false;

        // Возвращаем в исходное положение
        transform.position = initialPosition;
        Debug.Log("Бурение остановлено");
    }

    /// <summary>
    /// Переключить состояние бурения
    /// </summary>
    public void ToggleDrilling()
    {
        if (isRotating)
        {
            StopDrilling();
        }
        else
        {
            StartDrilling();
        }
    }

    /// <summary>
    /// Установить скорость вращения
    /// </summary>
    /// <param name="speed">Новая скорость вращения</param>
    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }

    /// <summary>
    /// Установить силу вибрации
    /// </summary>
    /// <param name="strength">Новая сила вибрации</param>
    public void SetVibrationStrength(float strength)
    {
        vibrationStrength = strength;
    }

    public void Pause() => OnPause = true;

    public void Play()
    {
        OnPause = false;
        initialPosition = transform.position;
    }
}