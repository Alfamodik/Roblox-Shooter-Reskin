using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections.Generic;
using UnityEngine;

public static class FadingOf
{
    private static List<TweenerCore<float, float, FloatOptions>> _tweens = new();

    public static void ChangeFading(CanvasGroup canvasGroup, float endFading, float duration)
        => _tweens.Add(canvasGroup.DOFade(endFading, duration));

    public static void ChangeFading(CanvasGroup canvasGroup, float endFading, float duration, Ease ease)
        => _tweens.Add(canvasGroup.DOFade(endFading, duration).SetEase(ease));

    public static void ChangeFading(AudioSource audioSource, float endFading, float duration)
        => _tweens.Add(audioSource.DOFade(endFading, duration));

    public static void Dispose()
    {
        for(int i = 0; i < _tweens.Count; i++)
            _tweens[i].Kill();
    }
}
