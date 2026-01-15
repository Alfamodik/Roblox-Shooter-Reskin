using System;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public event Action WaveActivated;
    public event Action WaveDeactivated;

    private IRankNotifier _rankNotifier;
    private EnemySpawner _enemySpawner;

    public void Initialize(EnemySpawner enemySpawner, IRankNotifier rankNotifier)
    {
        _enemySpawner = enemySpawner;
        _rankNotifier = rankNotifier;

        _rankNotifier.OnRankUpgraded += CooldownWave;
    }

    public void StartWork()
    {
        _enemySpawner.StartWork();
        WaveActivated?.Invoke();
    }

    private void CooldownWave(int rank)
    {
        _enemySpawner.StopWork();
        WaveDeactivated?.Invoke();
    }
}
