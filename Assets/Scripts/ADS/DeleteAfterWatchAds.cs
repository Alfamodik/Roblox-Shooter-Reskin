using UnityEngine;

public class DeleteAfterWatchAds : MonoBehaviour
{
    [SerializeField] private int _weaponIndex;
    [SerializeField] private GameObject _target;

    private INotifyingOfWeaponAdvertisement _notifier;

    public void Initialize(INotifyingOfWeaponAdvertisement notifier)
    {
        if (notifier.WeaponIndex == _weaponIndex)
        {
            _notifier = notifier;
            _notifier.WeaponAvailable += DeleteGameObject;
        }
    }

    private void OnDestroy()
    {
        if(_notifier != null)
            _notifier.WeaponAvailable -= DeleteGameObject;
    }

    private void DeleteGameObject(int weaponIndex)
    {
        print("TryDeleteAdImage");

        if(_weaponIndex == weaponIndex)
        {
            print("AdDeleteImage");
            Destroy(_target);
        }
    }
}
