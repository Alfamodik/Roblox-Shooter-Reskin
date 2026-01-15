using System;

public class RankCounter : IDisposable
{
    public event Action<int> RankUpgraded;

    private int _rank;
    private WaveController _waveController;

    public RankCounter(WaveController waveController)
    {
        _waveController = waveController;
        _waveController.WaveDeactivated += UpgradeRunk;

        RankUpgraded?.Invoke(_rank);
    }

    public void Dispose() => _waveController.WaveDeactivated -= UpgradeRunk;

    private void UpgradeRunk()
    {
        _rank++;
        RankUpgraded?.Invoke(_rank);
    }
}
