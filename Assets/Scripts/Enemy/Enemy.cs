using System;

public interface IEnemy
{
    event Action<IEnemy> IDie;

    void Kill();
}
