using Invector.vCharacterController;
using UnityEngine;
using YG;

public class CursorController : MonoBehaviour
{
    [SerializeField] private bool _lockOnAwake = true;
    [SerializeField] private vThirdPersonInput _characterInput;

    private void Awake()
    {
        if (_lockOnAwake)
            LockCursor();
    }

    private void OnEnable()
    {
        CharacterSkinChanger.CharacterChanged += OnCharacterChanged;
    }

    private void OnDisable()
    {
        CharacterSkinChanger.CharacterChanged -= OnCharacterChanged;
    }

    private void OnCharacterChanged(vThirdPersonInput characterInput)
    {
        _characterInput = characterInput;
    }

    public void LockCursor()
    {
        if (YG2.envir.isDesktop)
        {
            _characterInput.ShowCursor(false);
            _characterInput.LockCursor(false);
        }
    }

    public void UnlockCursor()
    {
        _characterInput.ShowCursor(true);
        _characterInput.LockCursor(true);
    }
}
