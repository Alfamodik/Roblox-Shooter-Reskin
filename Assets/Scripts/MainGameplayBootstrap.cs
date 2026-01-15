using GamePush;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MainGameplayBootstrap : MonoBehaviour
{
    [NonSerialized] public bool _pauseAvailable = true;

    [SerializeField] private string _currentVersion;

    [Space(30)]
    [SerializeField] private bool _mobieMod = false;
    [SerializeField] private int _pointToRankUpgrade;
    [SerializeField] private int _addRequirementsRankUpgrade;

    [Space(20)]
    [SerializeField, Range(0, 50)] private int _desktopInstanceLimit;
    [SerializeField, Range(0, 50)] private int _mobileInstanceLimit;

    [Space(20f)]
    [SerializeField] private UIKilledEnemy _UIKilledEnemy;

    [Space(20)]
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private WaveController _waveController;

    [Space(20)]
    [SerializeField] private PauseController _pauseController;
    [SerializeField] private AudioSource _victorySource;

    [Space(20)]
    [SerializeField] private List<NeedOfRankNotifier> _needOfRankNotifierList;

    private int _startRank = 0;
    private int _startScore = 0;

    private EnemyList _enemylist;
    private RankCounter _rankCounter;
    private RankUpgradeCondtion _rankUpgradeCondtion;

    private void Awake()
    {
#if UNITY_EDITOR
        LockUnlockCursor.NotInEditor = _mobieMod;
#endif

        if(PlayerPrefs.GetString("version", "") != _currentVersion)
            DeleteAllPrefs();

        PlayerPrefs.SetString("version", _currentVersion);

        //PlayerPrefs.SetString(EqipWeapon.CurrentWeaponKey, $"{Weapons.Shotgun}");

        //PlayerPrefs.SetInt("score", 4);
        //PlayerPrefs.SetInt("pointToRankUpgrade", 5);
        //PlayerPrefs.SetInt("rank", 50);

        _startRank = PlayerPrefs.GetInt(nameof(PlayerPrefsKeys.Rank), 0);
        _startScore = PlayerPrefs.GetInt(nameof(PlayerPrefsKeys.Score), 0);
        _pointToRankUpgrade = PlayerPrefs.GetInt(nameof(PlayerPrefsKeys.PointToRankUpgrade), _pointToRankUpgrade);

        CreateScripts();
        Initialize();

        GlobalAudio.AddSource(_victorySource);

        _waveController.WaveDeactivated += WaveDeactivated;
        _rankCounter.RankUpgraded += PlayWinSound;
    }

    private void Start()
    {
        GP_Game.GameReady();
        GP_Game.GameplayStart();
    }

    private void OnDestroy() => DisposeHandler.DisposeAll();


    [ContextMenu("DeleteAllPrefs")]
    public void DeleteAllPrefs() => PlayerPrefs.DeleteAll();

    public void SetPauseByHandler() => PauseHandler.SetPause(true);

    public void KillAllEnemy() => _enemylist.KillAllEnemy();

    private void PlayWinSound(int obj) => _victorySource.Play();

    private void WaveDeactivated()
    {
        print("WaveDeactivated");
        _pauseController.SetPause(true);
    }

    private void CreateScripts()
    {
        _enemylist = new();
        _rankCounter = new(_waveController);
        _rankUpgradeCondtion = new(_enemylist,_startRank, _startScore, _pointToRankUpgrade, _addRequirementsRankUpgrade);
    }

    private void Initialize()
    {
        if(GP_Device.IsMobile() && LockUnlockCursor.NotInEditor)
        {
            _enemySpawner.Initialize(_enemylist, _mobileInstanceLimit);
            print($"instanceLimit = {_mobileInstanceLimit}");
        }
        else
        {
            _enemySpawner.Initialize(_enemylist, _desktopInstanceLimit);
            print($"instanceLimit = {_desktopInstanceLimit}");
        }


        _waveController.Initialize(_enemySpawner, _rankUpgradeCondtion); //first
        _UIKilledEnemy.Initialize(_rankUpgradeCondtion);

        foreach (var item in _needOfRankNotifierList) //second
            item.Initialize(_rankUpgradeCondtion);

        _rankUpgradeCondtion.Invoke();
        _waveController.StartWork();
    }
}
