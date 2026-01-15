using GamePush;
using Invector.vCharacterController;
using UnityEngine;

public class DeviceTypeBootstrap : MonoBehaviour
{
    [SerializeField] private vShooterMeleeInput _VShooterMeleeInput;
    [SerializeField] private GameObject _mobileUI;
    [SerializeField] private GameObject _desktopUI;

    private void Start()
    {
        if(GP_Device.IsMobile() && LockUnlockCursor.NotInEditor)
        {
            _mobileUI.SetActive(true);
            _desktopUI.SetActive(false);
            LockUnlockCursor.ShowCursor(_VShooterMeleeInput);
        }
        else
        {
            _mobileUI.SetActive(false);
            _desktopUI.SetActive(true);
            LockUnlockCursor.HideCursor(_VShooterMeleeInput);
        }
    }
}
