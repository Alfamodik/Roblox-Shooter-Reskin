using System;
using System.Collections.Generic;

public class EnemyList : IDeathNotifier
{
    public event Action OnDied;

    private List<IEnemy> _list = new List<IEnemy>();

    public int Count => _list.Count;

    public void KillAllEnemy()
    {
        foreach (var item in _list)
            item.Kill();

        _list.Clear();
    }

    public void Add(IEnemy enemy)
    {
        _list.Add(enemy);
        enemy.IDie += AnyDie;
    }

    public void AnyDie(IEnemy enemy)
    {
        OnDied?.Invoke();
        enemy.IDie -= AnyDie;

        _list.Remove(enemy);
    }
}
