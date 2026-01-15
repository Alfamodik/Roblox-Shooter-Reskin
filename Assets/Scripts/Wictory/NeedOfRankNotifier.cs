using UnityEngine;

public class NeedOfRankNotifier: MonoBehaviour
{
    protected IRankNotifier RankNotifier;

    public virtual void Initialize(IRankNotifier rankNotifier) => RankNotifier = rankNotifier;
}
