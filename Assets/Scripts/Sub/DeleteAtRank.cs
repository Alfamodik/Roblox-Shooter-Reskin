using UnityEngine;

public class DeleteAtRank : NeedOfRankNotifier
{
    [SerializeField] private int _waitRank;
    [SerializeField] private GameObject _target;

    public override void Initialize(IRankNotifier rankNotifier)
    {
        RankNotifier = rankNotifier;
        RankNotifier.OnRankUpgraded += TryDelete;
    }

    private void OnDestroy()
    {
        if (RankNotifier != null)
            RankNotifier.OnRankUpgraded -= TryDelete;
    }

    private void TryDelete(int rank)
    {
        if(rank >= _waitRank)
            Destroy(_target);
    }
}
