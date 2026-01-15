using Invector.vCharacterController;
using UnityEngine;

[RequireComponent(typeof(vThirdPersonController),
    typeof(vShooterMeleeInput),
    typeof(Animator))]
public class Player : MonoBehaviour, IPauseble
{
    private vThirdPersonController _vThirdPersonController;
    private vShooterMeleeInput _vShooterMeleeInput;
    private Animator _animator;

    private bool _onPause = false;
    private float _lastSpeedMultiplier;
    private float _lastAnimatorSpeed;

    private void Awake()
    {
        _vThirdPersonController = GetComponent<vThirdPersonController>();
        _vShooterMeleeInput = GetComponent<vShooterMeleeInput>();
        _animator = GetComponent<Animator>();

        _lastSpeedMultiplier = _vThirdPersonController.speedMultiplier;
        _lastAnimatorSpeed = _animator.speed;

        PauseHandler.Add(this);
    }

    private void Update()
    {
        if(_onPause)
        {
            _animator.speed = 0;
            _vThirdPersonController.speedMultiplier = 0f;

            _vShooterMeleeInput.lockCameraInput = true;
        }
        else
        {
            _animator.speed = _lastAnimatorSpeed;
            _vThirdPersonController.speedMultiplier = _lastSpeedMultiplier;

            _vShooterMeleeInput.lockCameraInput = false;
        }
    }

    public void SetPause(bool flag)
    {
        if (_onPause == false)
        {
            _lastSpeedMultiplier = _vThirdPersonController.speedMultiplier;
            _lastAnimatorSpeed = _animator.speed;
        }

        _onPause = flag;
    }
}
