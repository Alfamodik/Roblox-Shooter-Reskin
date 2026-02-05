using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using YG.LanguageLegacy;

[RequireComponent(typeof(TMP_Text))]
public class UIKilledEnemy : MonoBehaviour
{
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _maxScale;

    [Space]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private LanguageYG _language;

    private int _score = 0;
    private string _startText;

    private TMP_Text _TMP_Text;
    private IScoreNotifier _scoreNotifier;

    private Vector3 _startScale;
    private RectTransform _rectTransform;
    private bool _isInitialized;

    public void Initialize(IScoreNotifier scoreNotifier)
    {
        print("UIKilledEnemy start Initialize");
        _TMP_Text = GetComponent<TMP_Text>();
        _rectTransform = GetComponent<RectTransform>();

        _scoreNotifier = scoreNotifier;
        _scoreNotifier.OnScoreChanged += OnScoreChange;

        _startText = _TMP_Text.text;
        _startScale = _rectTransform.localScale;

        StartCoroutine(InitializeCoroutine());
        print("UIKilledEnemy end Initialize");
    }

    private IEnumerator InitializeCoroutine()
    {
        yield return new WaitForSeconds(0.1f);

        Destroy(_language);
        
        if (_isInitialized) 
            yield break;

        _TMP_Text.text = $"{_startText}{_score}/{_scoreNotifier.PointToRankUpgrade}";
    }

    private void OnDestroy()
    {
        if(_scoreNotifier != null)
            _scoreNotifier.OnScoreChanged -= OnScoreChange;
    }

    private void OnScoreChange(int score)
    {
        if (!_isInitialized)
        {
            _isInitialized = true;
            Destroy(_language);
        }

        _score = score;
        _TMP_Text.text = $"{_startText}{_score}/{_scoreNotifier.PointToRankUpgrade}";

        StartCoroutine(UpDownScale());
    }

    private IEnumerator UpDownScale()
    {
        _rectTransform.localScale = _startScale;

        DoTween.ChangeScale(_rectTransform, new Vector3(_maxScale, _maxScale, _maxScale),
            _animationDuration, Ease.Linear, UpdateType.Normal, true);

        //������������ ��� ������ Invoke
        _audioSource?.Play();
        yield return new WaitForSeconds(_animationDuration);

        DoTween.ChangeScale(_rectTransform, _startScale,
            _animationDuration, Ease.Linear, UpdateType.Normal, true);
    }
}
