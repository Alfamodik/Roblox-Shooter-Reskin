using System;

public interface IScoreNotifier
{
    event Action<int> OnScoreChanged;

    public int PointToRankUpgrade { get; }
}