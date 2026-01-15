using System;
using UnityEngine;

public class RankUpgradeCondtion : IScoreNotifier, IRankNotifier, IDisposable
{
    public const string RankKey = nameof(PlayerPrefsKeys.Rank);
    public const string ScoreKey = nameof(PlayerPrefsKeys.Score);
    public const string PointToRankUpgradeKey = nameof(PlayerPrefsKeys.PointToRankUpgrade);

    public event Action<int> OnScoreChanged;
    public event Action<int> OnRankUpgraded;

    private int _pointToRankUpgrade;
    private int _addRequirementsRankUpgrade;

    private int _rank = 0;
    private int _score = 0;
    private IDeathNotifier _deathNotifier;

    public RankUpgradeCondtion(IDeathNotifier deathNotifier, int rank, int score, int pointToRankUpgrade, int addRequirementsRankUpgrade)
    {
        _rank = rank;
        _score = score;
        _pointToRankUpgrade = pointToRankUpgrade;
        _addRequirementsRankUpgrade = addRequirementsRankUpgrade;

        _deathNotifier = deathNotifier;
        _deathNotifier.OnDied += AddScore;

        DisposeHandler.Add(this);
    }


    public void Invoke()
    {
        OnRankUpgraded?.Invoke(_rank);
        OnScoreChanged?.Invoke(_score);
    }

    public int Rank => _rank;

    public int PointToRankUpgrade => _pointToRankUpgrade;

    public void Dispose() => _deathNotifier.OnDied -= AddScore;

    private void AddScore()
    {
        _score++;

        if(_score >= _pointToRankUpgrade)
        {
            _rank++;
            _score = 0;

            OnRankUpgraded?.Invoke(_rank);
            PlayerPrefs.SetInt(RankKey, _rank);

            _pointToRankUpgrade += _addRequirementsRankUpgrade;
            PlayerPrefs.SetInt(PointToRankUpgradeKey, _pointToRankUpgrade);
        }

        PlayerPrefs.SetInt(ScoreKey, _score);
        OnScoreChanged?.Invoke(_score);
    }
}
