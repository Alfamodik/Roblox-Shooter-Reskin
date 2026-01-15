using System;

public interface INotifyingOfWeaponAdvertisement
{
    public event Action<int> WeaponSelected;
    public event Action<int> WeaponAvailable;

    public int WeaponIndex { get; }
}
