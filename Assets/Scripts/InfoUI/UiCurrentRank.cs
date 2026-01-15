using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCurrentRank : NeedOfRankNotifier
{
    [SerializeField] private int _repeatCount;
    [SerializeField] private List<TMP_Text> _texts;

    [Space]
    [SerializeField] private List<GameObject> _images;
    [SerializeField] private List<Sprite> _rankSprites;

    public override void Initialize(IRankNotifier rankNotifier)
    {
        base.Initialize(rankNotifier);
        RankNotifier.OnRankUpgraded += OnRankUpgraded;
    }

    private void OnDestroy()
    {
        if (RankNotifier != null)
            RankNotifier.OnRankUpgraded -= OnRankUpgraded;
    }

    private void OnRankUpgraded(int rank)
    {
        foreach (var tmp_text in _texts)
            tmp_text.text = rank.ToString();

        int imageIndex = rank / _repeatCount;

        while(imageIndex >= _rankSprites.Count)
            imageIndex -= _rankSprites.Count;

        for (int i = 0; i < _images.Count; i++)
            _images[i].GetComponent<Image>().sprite = _rankSprites[imageIndex];
    }
}
