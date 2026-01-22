using UnityEngine;
using YG;

public class CursorController : MonoBehaviour
{
    [SerializeField] private bool _lockOnAwake = true;

    private void Awake()
    {
        if (_lockOnAwake)
            LockCursor();
    }

    public static void LockCursor()
    {
        if (YG2.envir.isDesktop)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public static void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
