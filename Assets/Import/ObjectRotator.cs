using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    [Header("Настройки вращения")]
    public float rotationSpeed = 30f; // Скорость вращения
    public Vector3 rotationAxis = Vector3.up; // Ось вращения (по умолчанию - вверх)
    public bool rotateOnStart = true; // Автоматическое вращение при старте
    
    private bool isRotating;

    void Start()
    {
        isRotating = rotateOnStart;
    }

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(rotationAxis * (rotationSpeed * Time.deltaTime));
        }
    }

    // Методы для управления из других скриптов
    public void StartRotating()
    {
        isRotating = true;
    }

    public void StopRotating()
    {
        isRotating = false;
    }

    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }
}