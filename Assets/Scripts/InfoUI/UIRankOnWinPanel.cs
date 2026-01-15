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
        _scoreViueOnWinPanel = GetComponent<TMP_Text>();
        _startText = _scoreViueOnWinPanel.text;

        _rankNotifier = rankNotifier;
        _rankNotifier.OnRankUpgraded += OnRankUpgraded;
    }

    private void OnRankUpgraded(int rank) => _scoreViueOnWinPanel.text = _startText + rank;
}
