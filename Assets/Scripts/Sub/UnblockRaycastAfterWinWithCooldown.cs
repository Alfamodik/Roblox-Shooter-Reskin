using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnblockRaycastAfterWinWithCooldown : NeedOfRankNotifier
{
    [SerializeField] private Image _target;
    [SerializeField] private float _durationToUnlock;

    public override void Initialize(IRankNotifier notifier)
    {
        base.Initialize(notifier);
        RankNotifier.OnRankUpgraded += Block;
    }

    private void Block(int rank)
    {
        if(_target.gameObject.activeInHierarchy)
        {
            _target.raycastTarget = false;
            StartCoroutine(Unblock());
        }
    }

    private IEnumerator Unblock()
    {
        yield return new WaitForSeconds(_durationToUnlock);
        _target.raycastTarget = true;
    }
}
