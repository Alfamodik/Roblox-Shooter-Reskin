using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAlpha : MonoBehaviour
{
    [SerializeField] private List<Image> _images;
    [SerializeField, Range(0, 1)] private float _startValue;
    [SerializeField, Range(0, 1)] private float _endValue;
    [SerializeField] private float _duration;

    public void SetAlpha(int index)
    {
        if(index < 0 || index >= _images.Count)
            throw new ArgumentOutOfRangeException($"{nameof(index)} должен быть в границах list");

        foreach(var image in _images)
            image.DOColor(new Color(image.color.r, image.color.g, image.color.b, _startValue), _duration)
                .SetUpdate(UpdateType.Normal, true);

        var img = _images[index];
        img.DOColor(new Color(img.color.r, img.color.g, img.color.b, _endValue), _duration)
            .SetUpdate(UpdateType.Normal, true);
    }
}
