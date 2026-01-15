using UnityEngine;

public class ActivateObjectAtRunk : MonoBehaviour
{
    [SerializeField] private int _waitRank;
    [SerializeField] private GameObject _target;

    private IRankNotifier _rankNotifier;

    public void Initialize(IRankNotifier rankNotifier)
    {
        _rankNotifier = rankNotifier;
        _rankNotifier.OnRankUpgraded += TryActivate;

        _target.SetActive(false);
    }

    private void TryActivate(int runk)
    {
        if(runk >= _waitRank)
        {
            _target.SetActive(true);
            _rankNotifier.OnRankUpgraded -= TryActivate;
        }
    }
}
