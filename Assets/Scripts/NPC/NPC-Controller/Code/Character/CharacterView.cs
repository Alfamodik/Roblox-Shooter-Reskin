using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int isRunningKey = Animator.StringToHash("isRunning");
    private readonly int isCraftingKey = Animator.StringToHash("isCrafting");
    [SerializeField] private Animator _animator;
    [SerializeField] private Character _character;

    private void Update()
    {
        if (_character.CurrentVelocity.magnitude > 0.05f)
            StartRunning();
        else
            StopRunning();
    }

    private void StartRunning()
    {
        _animator.SetBool(isRunningKey, true);
    }
    private void StopRunning()
    {
       _animator.SetBool(isRunningKey, false);
    }

    public void StartCrafting()
    {
        _animator.SetBool(isCraftingKey, true);
    }

    public void StopCrafting()
    {
        _animator.SetBool(isCraftingKey, false);
    }
}
