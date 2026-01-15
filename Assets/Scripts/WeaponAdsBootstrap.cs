using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAdsBootstrap : MonoBehaviour
{
    [SerializeField] private EqipWeapon _eqipWeapon;

    [SerializeField] private List<UnlockAfterWatch> _unlockAfterWatcheList;
    [SerializeField] private List<DeleteAfterWatchAds> _deleteAfterWatchAdsList;

    private void Start() => StartCoroutine(Initialize());

    private IEnumerator Initialize()
    {
        yield return null;

        _eqipWeapon.Initialize();

        foreach(UnlockAfterWatch unlockAfterWatch in _unlockAfterWatcheList)
        {
            foreach(DeleteAfterWatchAds deleteAfterWatchAds in _deleteAfterWatchAdsList)
                deleteAfterWatchAds.Initialize(unlockAfterWatch);
        }

        foreach(UnlockAfterWatch unlockAfterWatch in _unlockAfterWatcheList)
        {
            unlockAfterWatch.Initialize(_eqipWeapon);
        }
    }
}
