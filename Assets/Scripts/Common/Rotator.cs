using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _rotationSpeed;
    [SerializeField] private float _startRotation;

    private float _currentRotation = 0;

    public void Update()
    {
        _currentRotation -= Time.deltaTime * _rotationSpeed;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, _currentRotation, transform.rotation.eulerAngles.z);
    }

    public void ResetRotation() => _currentRotation = _startRotation;
}