using System;

public interface IRankNotifier
{
    event Action<int> OnRankUpgraded;
}