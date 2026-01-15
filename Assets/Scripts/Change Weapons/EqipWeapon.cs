using Invector.vItemManager;
using System;
using UnityEngine;

public enum Weapons
{   
    HandGun = 10,
    Shotgun = 12,
    AssaultRifle = 31,
    P90 = 11,
    Rpg = 16,
}

public enum AmmoID
{
    ShotGun = 17,
    AssaultRifle = 13,
    P90 = 19,
    RPG = 18,
}

public class EqipWeapon : MonoBehaviour
{
    public const string CurrentWeaponKey = nameof(PlayerPrefsKeys.CurrentWeapon);

    [SerializeField] private vItemManager _playerItemManager;
    [SerializeField] private GameObject mixamorigRightHand;

    private Weapons _curentWeapon = Weapons.HandGun;

    public void Initialize()
    {
        string savedWeapon = PlayerPrefs.GetString(CurrentWeaponKey);
        print($"Current weapon = {savedWeapon}");

        if(string.IsNullOrEmpty(savedWeapon))
        {
            SetWeapon(Weapons.HandGun, AmmoID.RPG);
            return;
        }

        _curentWeapon = (Weapons)Enum.Parse(typeof(Weapons), savedWeapon);
        SetWeapon(_curentWeapon, AmmoID.ShotGun);
    }

    public void SetShotGun() => SetWeapon(Weapons.Shotgun, AmmoID.ShotGun);

    public void SetAssaultRifle() => SetWeapon(Weapons.AssaultRifle, AmmoID.AssaultRifle);

    public void SetP90() => SetWeapon(Weapons.P90, AmmoID.P90);

    public void SetRpg() => SetWeapon(Weapons.Rpg, AmmoID.RPG);

    public void DestroyAllWeapons()
    {
        _playerItemManager.DestroyAllItems();

        var defaultHandler = mixamorigRightHand
            .transform
            .GetChild(2)
            .GetChild(0)
            .GetChild(0);

        for (int i = 0; i < defaultHandler.childCount; i++)
            Destroy(defaultHandler.GetChild(i).gameObject);
    }

    private void SetWeapon(Weapons weapon, AmmoID ammoID)
    {
        DestroyAllWeapons();

        ItemReference newWeapon = new((int)weapon)
        {
            amount = 1,
            autoEquip = true,
            addToEquipArea = true,
        };

        ItemReference ammo = new((int)ammoID)
        {
            amount = 1000,
            autoEquip = true,
            addToEquipArea = true,
        };

        _playerItemManager.AddItem(newWeapon);
        _playerItemManager.AddItem(ammo);
        _curentWeapon = weapon;

        PlayerPrefs.SetString(CurrentWeaponKey, $"{_curentWeapon}");

        print($"{_curentWeapon} seted");
        print($"{_curentWeapon} saved");
    }
}
