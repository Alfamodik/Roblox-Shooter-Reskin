using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect), typeof(RectTransform))]
public class ScrollRank : NeedOfRankNotifier
{
    public const float ScrollOffset = -30;

    [SerializeField] private List<Sprite> _rankSprites;
    [SerializeField] private GameObject _rankPrefab;
    [SerializeField] private Transform _rankParent;
    [SerializeField] private int _stockOfItems;

    [Space]
    [SerializeField] private float _step;
    [SerializeField] private float _normalSize;
    [SerializeField] private float _selectedSize;

    private List<Transform> _ranks = new();
    private ScrollRect _scrollRect;

    public override void Initialize(IRankNotifier rankNotifier)
    {
        base.Initialize(rankNotifier);

        _scrollRect = GetComponent<ScrollRect>();
        RankNotifier.OnRankUpgraded += WaveDeactivated;
    }

    private void OnDestroy()
    {
        if(RankNotifier != null)
            RankNotifier.OnRankUpgraded -= WaveDeactivated;
    }

    private void WaveDeactivated(int rank)
    {
        CreateNecessaryRanks(rank);
        SetNormalScale();

        ScrollTo(rank);
        ScaleCurrentRank(rank);
    }

    private void ScrollTo(int rank)
    {
        _scrollRect.content.localPosition = new Vector3(-_step * (rank - 1), 0, 0);
        _scrollRect.content.DOLocalMove(new Vector3(-_step * rank + ScrollOffset, 0, 0), 1);
    }

    private void ScaleCurrentRank(int rank)
        => _ranks[rank].DOScale(new Vector2(_selectedSize, _selectedSize), 1)
        .SetUpdate(UpdateType.Normal, true);

    private void CreateNecessaryRanks(int rank)
    {
        var needCount = rank + _stockOfItems;

        for(int i = _ranks.Count; i < needCount; i++)
        {
            var instance = Instantiate(_rankPrefab, Vector3.zero, new Quaternion(0, 0, 0, 0), _rankParent);
            instance.transform.GetChild(1).GetComponent<TMP_Text>().text = _ranks.Count.ToString();

            int imageIndex = i / 5;

            while(imageIndex >= _rankSprites.Count)
                imageIndex -= _rankSprites.Count;

            instance.GetComponent<Image>().sprite = _rankSprites[imageIndex];
            _ranks.Add(instance.transform);
        }
    }

    private void SetNormalScale()
    {
        foreach(var item in _ranks)
            item.localScale = new Vector3(_normalSize, _normalSize, _normalSize);
    }
}
