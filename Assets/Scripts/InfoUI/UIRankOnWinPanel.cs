using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class UIRankOnWinPanel : MonoBehaviour
{
    private TMP_Text _scoreViueOnWinPanel;

    private string _startText;
    private IRankNotifier _rankNotifier;

    public void Initialize(IRankNotifier rankNotifier)
    {
        print("UIRankOnWinPanel start Initialize");
        _scoreViueOnWinPanel = GetComponent<TMP_Text>();
        _startText = _scoreViueOnWinPanel.text;

        _rankNotifier = rankNotifier;
        _rankNotifier.OnRankUpgraded += OnRankUpgraded;
        print("UIRankOnWinPanel end Initialize");
    }

    private void OnRankUpgraded(int rank) => _scoreViueOnWinPanel.text = _startText + rank;
}
