using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using YG;
using DG.Tweening;

public class ResultsPanel : MonoBehaviour
{
    private const string MainScene = "HubScene";

    [SerializeField] private ResourcesHolder _playerResourcesHolder;
    [SerializeField] private ResourcesHolder _enemyResourcesHolder;

    [SerializeField] private GameObject VictoryPanel;
    [SerializeField] private GameObject LoosePanel;
    [SerializeField] private GameObject Joystick;

    [SerializeField] private Button VictoryButton;
    [SerializeField] private Button LooseButton;
    [SerializeField] private int RewardAmount;
    
    [Header("Animation Settings")]
    [SerializeField] private float _animationDuration = 0.5f;
    [SerializeField] private Ease _animationEase = Ease.OutBack;

    private bool _allreadyShowing;

    private void Awake()
    {
        _playerResourcesHolder.OnTargetAchieved += ShowVictoryPanel;
        _enemyResourcesHolder.OnTargetAchieved += ShowLoosePanel;

        VictoryButton.onClick.AddListener(LoadMainScene);
        VictoryButton.onClick.AddListener(GiveReward);
        VictoryButton.onClick.AddListener(PlaySound);

        LooseButton.onClick.AddListener(LoadMainScene);
        LooseButton.onClick.AddListener(PlaySound);
    }


    private void PlaySound()
    {
        SFXProvider.Play("SFX_UI_Button_Click_Select");
    }

    private void LoadMainScene()
    {
        //if (YG2.isTimerAdvCompleted)
        //    YG2.InterstitialAdvShow();

        PauseHandler.Play();
        CursorController.LockCursor();

        //YG2.InterstitialAdvShow();
        SceneManager.LoadSceneAsync(MainScene);
    }

    private void ShowVictoryPanel()
    {
        if (_allreadyShowing)
            return;

        _allreadyShowing = true;
        Joystick.SetActive(false);
        VictoryPanel.SetActive(true);
        VictoryPanel.transform.localScale = Vector3.zero;
        VictoryPanel.transform.DOScale(1f, _animationDuration).SetEase(_animationEase).SetUpdate(true);
        CursorController.UnlockCursor();
        PauseHandler.Pause();
    }

    private void ShowLoosePanel()
    {
        if (_allreadyShowing)
            return;

        _allreadyShowing = true;
        Joystick.SetActive(false);
        LoosePanel.SetActive(true);
        LoosePanel.transform.localScale = Vector3.zero;
        LoosePanel.transform.DOScale(1f, _animationDuration).SetEase(_animationEase).SetUpdate(true);
        CursorController.UnlockCursor();
        PauseHandler.Pause();
    }

    private void GiveReward()
    {
        Wallet.Instance.Add(RewardAmount);
    }
}
