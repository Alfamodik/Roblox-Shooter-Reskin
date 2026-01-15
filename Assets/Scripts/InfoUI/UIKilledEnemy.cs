using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class UIKilledEnemy : MonoBehaviour
{
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _maxScale;

    [Space]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SetLanguage _language;

    private int _score = 0;
    private string _startText;

    private TMP_Text _TMP_Text;
    private IScoreNotifier _scoreNotifier;

    private Vector3 _startScale;
    private RectTransform _rectTransform;

    public void Initialize(IScoreNotifier scoreNotifier)
    {
        _language.Initialize();

        _TMP_Text = GetComponent<TMP_Text>();

        _scoreNotifier = scoreNotifier;
        _scoreNotifier.OnScoreChanged += OnScoreChange;

        _startText = _TMP_Text.text;
        _TMP_Text.text = $"{_startText}{_score}/{_scoreNotifier.PointToRankUpgrade}";

        _rectTransform = GetComponent<RectTransform>();
        _startScale = _rectTransform.localScale;
    }

    private void OnDestroy()
    {
        if(_scoreNotifier != null)
            _scoreNotifier.OnScoreChanged -= OnScoreChange;
    }

    private void OnScoreChange(int score)
    {
        _score = score;
        _TMP_Text.text = $"{_startText}{_score}/{_scoreNotifier.PointToRankUpgrade}";

        StartCoroutine(UpDownScale());
    }

    private IEnumerator UpDownScale()
    {
        _rectTransform.localScale = _startScale;

        DoTween.ChangeScale(_rectTransform, new Vector3(_maxScale, _maxScale, _maxScale),
            _animationDuration, Ease.Linear, UpdateType.Normal, true);

        //Игнорировать при первом Invoke
        _audioSource?.Play();
        yield return new WaitForSeconds(_animationDuration);

        DoTween.ChangeScale(_rectTransform, _startScale,
            _animationDuration, Ease.Linear, UpdateType.Normal, true);
    }
}
