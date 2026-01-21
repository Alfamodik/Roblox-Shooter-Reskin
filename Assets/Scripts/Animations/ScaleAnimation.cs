using DG.Tweening;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _duration;
    [SerializeField] private float _from;
    [SerializeField] private float _to;

    [SerializeField] private Ease _ease;


    private void Start() => _target
            .DOScale(_to, _duration)
            .From(_from)
            .SetEase(_ease)
            .SetLoops(-1, LoopType.Yoyo)
            .SetRelative();
}
