using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class ActivateButtonAtRank : NeedOfRankNotifier, IDisposable
{
    [SerializeField] private Color _disableColor;

    [SerializeField] private float _fadeTime;
    [SerializeField] private int _waitRank;

    private Color _enaibleColor;

    private Image _image;
    private Button _button;

    public override void Initialize(IRankNotifier rankNotifier)
    {
        base.Initialize(rankNotifier);

        _button = GetComponent<Button>();
        _image = _button.gameObject.GetComponent<Image>();

        _enaibleColor = _image.color;
        _image.raycastTarget = false;
        _image.DOColor(_disableColor, _fadeTime).SetUpdate(UpdateType.Normal, true);

        RankNotifier.OnRankUpgraded += TryActivate;

        DisposeHandler.Add(this);
    }

    public void Dispose() => RankNotifier.OnRankUpgraded -= TryActivate;

    private void TryActivate(int runk)
    {
        if(runk >= _waitRank)
        {
            _image.raycastTarget = true;
            _image.DOColor(_enaibleColor, _fadeTime).SetUpdate(UpdateType.Normal, true);

            RankNotifier.OnRankUpgraded -= TryActivate;
        }
    }
}
