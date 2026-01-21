using System;
using UnityEngine;

public class DirectionalRotator : IPausable, IDisposable
{
    private Transform _transform;
    private float _rotationSpeed;
    private Vector3 _currentDirection;

    public bool OnPause { get; private set; }

    public DirectionalRotator(Transform transform, float rotationSpeed)
    {
        _transform = transform;
        _rotationSpeed = rotationSpeed;
    }

    public Quaternion CurrentRotation => _transform.rotation;

    public void SetInputDirection(Vector3 direction) => _currentDirection = direction;
    
    public void SetInstantRotation(Vector3 direction)
    {
        if (direction.magnitude < 0.05f) 
            return;

        _transform.rotation = Quaternion.LookRotation(direction.normalized);
        _currentDirection = direction;
    }
   
    public void Update(float deltaTime)
    {
        if (OnPause)
            return;

        if (_currentDirection.magnitude < 0.05)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(_currentDirection.normalized);

        float step = _rotationSpeed * deltaTime;
       _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
    }

    public void Pause() => OnPause = true;

    public void Play() => OnPause = false;

    public void Dispose() => PauseHandler.Remove(this);
}
