using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using TMPro;
using UnityEngine;

public class DisappearingMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private float _disappearingDuration;
    [SerializeField] private Vector3 _disappearingOffset;

    private Color _startColor;
    private Vector3 _startPosition;

    private TweenerCore<Color, Color, ColorOptions> _colorTweenerCore;
    private TweenerCore<Vector3, Vector3, VectorOptions> _moveTweenerCore;

    private void Awake()
    {
        _startColor = _messageText.color;
        _startPosition = _messageText.transform.position;
        
        _messageText.color = new Color(_startColor.r, _startColor.g, _startColor.b, 0);
    }

    public void ShowMessage(string text, Action onComplete = null)
    {
        _messageText.text = text;
        _messageText.color = _startColor;
        _messageText.transform.position = _startPosition;

        _colorTweenerCore?.Kill();
        _colorTweenerCore = _messageText
            .DOColor(new Color(_startColor.r, _startColor.g, _startColor.b, 0), _disappearingDuration)
            .OnComplete(() => _colorTweenerCore?.Kill());

        _moveTweenerCore?.Kill();
        _moveTweenerCore = _messageText.transform
            .DOMove(_messageText.transform.position + _disappearingOffset, _disappearingDuration)
            .OnComplete(() => _moveTweenerCore?.Kill())
            .OnComplete(() => onComplete?.Invoke());
    }
}
