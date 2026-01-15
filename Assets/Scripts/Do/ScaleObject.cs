using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    [SerializeField] private RectTransform _target;
    [SerializeField] private bool _enableOnStart;

    [SerializeField] private Vector3 _maxSize;
    [SerializeField] private float _duration;

    [SerializeField] private Ease ease;

    private Vector3 _startSize;
    private TweenerCore<Vector3, Vector3, VectorOptions> _tween;

    private void Awake()
    {
        _startSize = transform.localScale;

        if(_enableOnStart)
            StartCoroutine(InfinityScale());
    }

    private void OnEnable()
    {
        StartCoroutine(InfinityScale());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _tween.Kill();

        _target.localScale = _startSize;
    }

    public IEnumerator InfinityScale()
    {
        while(true)
        {
            yield return new WaitForSeconds(_duration);

            _tween = _target.DOScale(_maxSize, _duration)
                .SetEase(ease).SetUpdate(UpdateType.Normal, true);

            yield return new WaitForSeconds(_duration);

            _tween = _tween = _target.DOScale(_startSize, _duration)
                .SetEase(ease).SetUpdate(UpdateType.Normal, true);
        }
    }
}
