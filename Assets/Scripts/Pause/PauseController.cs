using GamePush;
using Invector.vCharacterController;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject _winCanvas;
    [SerializeField] private EnemySpawner _enemySpawner;

    [SerializeField] private vThirdPersonController _playerVThirdPersonController;
    [SerializeField] private vShooterMeleeInput _playerVShooterMeleeInput;

    [SerializeField] private List<AudioSource> _audioSources;

    private bool _pauseAvailable = true;

    private void Awake()
    {
        foreach (var item in _audioSources)
            GlobalAudio.AddSource(item);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && _pauseAvailable)
            SetPause(true);
    }

    public void SetPauseAvailable(bool available) => _pauseAvailable = available;

    public void SetPause(bool setActiveUI)
    {
        print($"SetPause, setActiveUI = {setActiveUI}");

        if(_pauseAvailable == false)
            return;

        _pauseAvailable = false;
        _enemySpawner.StopWork();

        if(setActiveUI)
        {
            _winCanvas.SetActive(true);
        }
        
        _playerVShooterMeleeInput.SetLockAllInput(true);

        LockUnlockCursor.ShowOnDesktop(_playerVShooterMeleeInput);

        PauseHandler.Pause();
    }

    public void TakeOfPause(bool setActiveUI)
    {
        print($"TakeOfPause, setActiveUI = {setActiveUI}");

        _pauseAvailable = true;
        _enemySpawner.StartWork();

        if(setActiveUI)
            _winCanvas.SetActive(false);

        _playerVShooterMeleeInput.SetLockAllInput(false);
        LockUnlockCursor.HideOnDesktop(_playerVShooterMeleeInput);

        PauseHandler.Play();
    }

    public void ChangedPause()
    {
        print("ChangedPause");

        if(_pauseAvailable)
        {
            SetPause(true);
            return;
        }

        TakeOfPause(true);
    }

    public void SetAdsPause(bool unlockCursor)
    {
        print("SetAdsPause");

        if (unlockCursor)
            LockUnlockCursor.ShowOnDesktop(_playerVShooterMeleeInput);
        
        GP_Game.Pause();
        Time.timeScale = 0f;
        AudioListener.volume = 0.0f;
        GlobalAudio.SetMuteSuorces(true);
    }

    public void TakeOfAdsPause(bool lockCursor)
    {
        print("TakeOfAdsPause");

        if (lockCursor)
            LockUnlockCursor.HideOnDesktop(_playerVShooterMeleeInput);

        GP_Game.Resume();
        Time.timeScale = 1.0f;
        AudioListener.volume = 1.0f;
        GlobalAudio.SetMuteSuorces(false);
    }
}
