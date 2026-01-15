using GamePush;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUnlock : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private bool _unlockOnStart;
    [SerializeField] private bool _deleteIfUnlocked;

    [Space]
    [SerializeField] private bool _selectAfterWatch;
    [SerializeField] private SelectWeapon _selectWeapon;

    [SerializeField] private Button _weaponButton;
    [SerializeField] private PauseController _pauseController;

    private void Awake()
    {
        if(_unlockOnStart)
        {
            WeaponDataProvider.Save(_weapon, true);
            
            if(_deleteIfUnlocked)
                Destroy(gameObject);

            return;
        }

        WeaponDataProvider.TryGet(_weapon, out bool isUnlocked);

        if(isUnlocked == false)
            _weaponButton.onClick.AddListener(ShowAds);

        else if(_deleteIfUnlocked)
            Destroy(gameObject);
    }

    private void OnDestroy() => _weaponButton.onClick.RemoveListener(ShowAds);

    private void ShowAds()
    {
#if UNITY_EDITOR
        StartAds();
        AwardReward("");
        EndAds(true);
#endif
        if(GP_Ads.IsRewardedAvailable())
            GP_Ads.ShowRewarded("", AwardReward, StartAds, EndAds);
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

        if(successful == false)
            return;

        WeaponDataProvider.Save(_weapon, true);

        if(_selectAfterWatch)
            _selectWeapon.Select();

        if(_deleteIfUnlocked)
            Destroy(gameObject);
    }
}
