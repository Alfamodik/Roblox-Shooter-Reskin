using Invector.vCharacterController;
using UnityEngine;
using YG;

public class CursorController : MonoBehaviour
{
    private vThirdPersonInput _characterInput;

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
        Debug.Log("LockCursor()");

        if (YG2.envir.isMobile)
            return;

        _characterInput.ShowCursor(false);
        _characterInput.LockCursor(false);
    }

    public void UnlockCursor()
    {
        Debug.Log("UnlockCursor()");

        if (YG2.envir.isMobile)
            return;

        _characterInput.ShowCursor(true);
        _characterInput.LockCursor(true);
    }
}
