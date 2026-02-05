using UnityEngine;

public class NeedOfRankNotifier: MonoBehaviour
{
    protected IRankNotifier RankNotifier;

    public virtual void Initialize(IRankNotifier rankNotifier)
    {
        print("NeedOfRankNotifier start Initialize");
        RankNotifier = rankNotifier;
        print("NeedOfRankNotifier end Initialize");
    }
}
