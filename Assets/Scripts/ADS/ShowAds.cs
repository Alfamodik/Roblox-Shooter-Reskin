using GamePush;
using UnityEngine;

public class ShowAD : NeedOfRankNotifier
{
    [SerializeField] private PauseController _pauseController;

    private bool _adsAviable = false;

    public override void Initialize(IRankNotifier rankNotifier)
    {
        base.Initialize(rankNotifier);
        RankNotifier.OnRankUpgraded += SetAviable;
    }

    public void TryShowFullscreen()
    {
        if(GP_Ads.IsFullscreenAvailable() && _adsAviable)
            ShowFullscreen();
    }

    private void SetAviable(int obj) => _adsAviable = true;

    private void ShowFullscreen() => GP_Ads.ShowFullscreen(OnStart, OnEnd);

    private void OnStart() => _pauseController.SetAdsPause(true);

    private void OnEnd(bool ended)
    {
        _adsAviable = false;
        _pauseController.TakeOfAdsPause(true);
        GP_Game.GameReady();
    }
}
