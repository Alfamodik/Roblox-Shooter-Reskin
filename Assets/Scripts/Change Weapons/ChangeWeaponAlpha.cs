using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeaponAlpha : MonoBehaviour
{
    [SerializeField] private List<Image> _images;
    [SerializeField] private List<UnlockAfterWatch> _unlockAfterWatch;

    [SerializeField, Range(0, 1)] private float _startValue;
    [SerializeField, Range(0, 1)] private float _endValue;
    [SerializeField] private float _duration;

    private void Awake()
    {
        foreach (var item in _unlockAfterWatch)
            item.WeaponSelected += SetWeaponAlpha;
    }

    private void OnDestroy()
    {
        foreach(var item in _unlockAfterWatch)
            item.WeaponSelected -= SetWeaponAlpha;
    }

    public void SetWeaponAlpha(int byWeaponId, bool apply = true)
    {
        if(apply == false)
            return;

        if(byWeaponId < 0 || byWeaponId >= _images.Count)
            throw new ArgumentOutOfRangeException($"{nameof(byWeaponId)} должен быть в границах list");

        foreach(var image in _images)
            image.DOColor(new Color(image.color.r, image.color.g, image.color.b, _startValue), _duration)
                .SetUpdate(UpdateType.Normal, true);

        var img = _images[byWeaponId];
        img.DOColor(new Color(img.color.r, img.color.g, img.color.b, _endValue), _duration)
            .SetUpdate(UpdateType.Normal, true);
    }

    public void SetWeaponAlpha(int byWeaponId) => SetWeaponAlpha(byWeaponId, true);
}
