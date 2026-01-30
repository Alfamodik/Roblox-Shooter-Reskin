using Invector.vCharacterController;
using UnityEngine;
using YG;

public class LockUnlockCursor : MonoBehaviour
{
    public static bool NotInEditor = true;

    public static void ShowCursor(vShooterMeleeInput VShooterMeleeInput)
    {
        VShooterMeleeInput.ShowCursor(true);
        VShooterMeleeInput.LockCursor(true);
    }

    public static void HideCursor(vShooterMeleeInput VShooterMeleeInput)
    {
        VShooterMeleeInput.ShowCursor(false);
        VShooterMeleeInput.LockCursor(false);
    }

    public static void ShowOnDesktop(vShooterMeleeInput VShooterMeleeInput)
    {
        if(IsDesktop())
            ShowCursor(VShooterMeleeInput);
    }

    public static void HideOnDesktop(vShooterMeleeInput VShooterMeleeInput)
    {
        if(IsDesktop())
            HideCursor(VShooterMeleeInput);
    }

    private static bool IsDesktop() => YG2.envir.isDesktop == NotInEditor;
}
