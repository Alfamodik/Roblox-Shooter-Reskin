using GamePush;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UnlockAfterWatch : MonoBehaviour, INotifyingOfWeaponAdvertisement
{
    public event Action<int> WeaponSelected;
    public event Action<int> WeaponAvailable;

    [SerializeField] private int _weaponIndex;
    [SerializeField] private Weapons _weapon;

    [SerializeField] private ChangeWeaponAlpha _changeWeaponAlpha;
    [SerializeField] private PauseController _pauseController;
    [SerializeField] private ReceivedWeaponsForAdvertising _receivedWeaponsForAdvertising;

    private bool _watched = false;
    private EqipWeapon _eqipWeapon;
    private Button _button;

    public int WeaponIndex => _weaponIndex;

    public void Initialize(EqipWeapon eqipWeapon)
    {
        _eqipWeapon = eqipWeapon;
        _button = GetComponent<Button>();

        _watched = _receivedWeaponsForAdvertising.GetData($"{_weapon}");
        print($"{_weapon} watched {_watched}");

        if(_watched == false)
        {
            _button.onClick.AddListener(OnClick);
            return;
        }

        print($"WeaponAvailable?.Invoke({_weaponIndex});");
        WeaponAvailable?.Invoke(_weaponIndex);

        string savedWeapon = PlayerPrefs.GetString(EqipWeapon.CurrentWeaponKey);

        if(string.IsNullOrEmpty(savedWeapon) == false)
        {
            if(_weapon == (Weapons)Enum.Parse(typeof(Weapons), savedWeapon))
            {
                print($"WeaponSelected?.Invoke({_weaponIndex});");
                WeaponSelected?.Invoke(_weaponIndex);
            }
        }

        SetLitener(false);
    }

    private void OnClick()
    {
        print($"OnWeaponClick {_weapon}");
        print($"_watched = {_watched}");

        if(_watched)
        {
            _button.onClick.RemoveListener(OnClick);
            return;
        }

#if UNITY_EDITOR
        StartAds();
        EndAds(true);
#endif

        if(GP_Ads.IsRewardedAvailable())
            GP_Ads.ShowRewarded("", AwardReward, StartAds, EndAds);
    }

    private void SetLitener(bool giveWeapon)
    {
        switch(_weapon)
        {
            case Weapons.Shotgun:
                _button.onClick.AddListener(_eqipWeapon.SetShotGun);

                if(giveWeapon)
                    _eqipWeapon.SetShotGun();

                break;

            case Weapons.AssaultRifle:
                _button.onClick.AddListener(_eqipWeapon.SetAssaultRifle);

                if(giveWeapon)
                    _eqipWeapon.SetAssaultRifle();

                break;

            case Weapons.P90:
                _button.onClick.AddListener(_eqipWeapon.SetP90);

                if(giveWeapon)
                    _eqipWeapon.SetP90();

                break;

            case Weapons.Rpg:
                _button.onClick.AddListener(_eqipWeapon.SetRpg);

                if(giveWeapon)
                    _eqipWeapon.SetRpg();

                break;

            default:
                throw new NotImplementedException("Данный метод не поддерживается!");
        }

        _button.onClick.AddListener(() => _changeWeaponAlpha.SetWeaponAlpha(_weaponIndex));
        _button.onClick.RemoveListener(OnClick);
    }

    private void AwardReward(string value) { }

    private void StartAds()
    {
        print("Start Rewarded Ads");
        _pauseController.SetAdsPause(false);
    }

    private void EndAds(bool successful)
    {
        print($"Rewarded Ads Ended, successful = {successful}");
        _pauseController.TakeOfAdsPause(false);

        GP_Game.GameReady();

        if(successful)
        {
            print($"OnAdsEnded {_weapon}");
            print($"_watched = {_watched}");

            _watched = true;

            print($"_watchedSet {_watched}");

            _receivedWeaponsForAdvertising.WeaponBought($"{_weapon}");

            SetLitener(true);

            print($"WeaponSelected?.Invoke({_weaponIndex});");
            print($"WeaponAvailable?.Invoke({_weaponIndex});");

            WeaponSelected?.Invoke(_weaponIndex);
            WeaponAvailable?.Invoke(_weaponIndex);
        }
    }
}
