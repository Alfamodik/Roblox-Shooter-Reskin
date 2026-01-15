using GamePush;
using Invector.vCharacterController;
using UnityEngine;

public class Revive : MonoBehaviour
{
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _loseCanvas;
    [SerializeField] private PauseController _pauseController;
    [SerializeField] private ProgressBackUp _backUp;

    public void RevivePlayer()
    {
        print("RevivePlayer");

#if UNITY_EDITOR
        Started();
        End(true);
#endif

        if(GP_Ads.IsRewardedAvailable())
            GP_Ads.ShowRewarded("Revive", e => { }, Started, End);
    }

    private void RevivePl()
    {
        print("RevivePl");

        vThirdPersonController controller = _player.GetComponent<vThirdPersonController>();
        vShooterMeleeInput vShooterMeleeInput = _player.GetComponent<vShooterMeleeInput>();

        controller.isDead = false;
        controller.lockMovement = false;

        controller.ResetHealth();
        controller.Init();

        vShooterMeleeInput.SetLockAllInput(false);
        _player.transform.position = _respawnPoint.position;
        _pauseController.SetPauseAvailable(true);

    }

    private void Started()
    {
        print("AdsStarted");
        _pauseController.SetAdsPause(true);
    }

    private void End(bool successfully)
    {
        print("AdsEnded");

        GP_Game.GameReady();

        if(successfully)
        {
            RevivePl();

            _backUp.LoadBackUp();
            _loseCanvas.SetActive(false);
            _pauseController.TakeOfAdsPause(true);
            _pauseController.TakeOfPause(true);
        }
    }
}
