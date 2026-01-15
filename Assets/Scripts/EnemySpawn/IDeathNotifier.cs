using System;

public interface IDeathNotifier
{
    public event Action OnDied;
}
