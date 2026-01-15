using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections.Generic;
using UnityEngine;

public static class DoTween
{
    private static List<TweenerCore<float, float, FloatOptions>> _tweens = new();
    private static List<TweenerCore<Vector3, Vector3, VectorOptions>> _vectorTweens = new();

    public static void ChangeFading(CanvasGroup canvasGroup, float endFading, float duration)
        => _tweens.Add(canvasGroup.DOFade(endFading, duration));

    public static void ChangeFading(CanvasGroup canvasGroup, float endFading, float duration, Ease ease)
        => _tweens.Add(canvasGroup.DOFade(endFading, duration).SetEase(ease));

    public static void ChangeScale(RectTransform rectTransform, Vector3 endValue, float duration, Ease ease, UpdateType updateType, bool isIndependentUpdate)
        => _vectorTweens.Add(rectTransform.DOScale(endValue, duration).SetEase(ease).SetUpdate(updateType, isIndependentUpdate));

    public static void ChangeFading(AudioSource audioSource, float endFading, float duration)
        => _tweens.Add(audioSource.DOFade(endFading, duration));

    public static void Dispose()
    {
        for(int i = 0; i < _tweens.Count; i++)
            _tweens[i].Kill();

        _tweens.Clear();

        for(int i = 0; i < _vectorTweens.Count; i++)
            _vectorTweens[i].Kill();

        _vectorTweens.Clear();
    }
}
