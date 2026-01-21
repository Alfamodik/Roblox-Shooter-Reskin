using System.Collections;
using UnityEngine;

[SelectionBase]
public class RandomizeDragonAnimations : MonoBehaviour, IPausable
{
    [SerializeField] private Animator _animator;

    [Space]
    [SerializeField] private string _valueName;
    [SerializeField] private string _transitionName;

    [Space]
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;

    [SerializeField] private float _minTransitionDelay;
    [SerializeField] private float _maxTransitionDelay;

    [Space]
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;

    [SerializeField] private float _minTransitionValue;
    [SerializeField] private float _maxTransitionValue;

    [Space]
    [SerializeField] private float _angleTolerance;
    [SerializeField] private float _minTimeForMovesAwayFromTheWall;
    [SerializeField] private float _maxTimeForMovesAwayFromTheWall;

    [Space]
    [SerializeField] private float _transitionForRotation;
    [SerializeField] private float _valueForRotation;
    [SerializeField] private float _transitionForWalking;
    [SerializeField] private float _valueForWalking;

    private float _targetValue;
    private float _transitionTargetValue;

    private bool _movesAwayFromTheWall;

    public bool OnPause { get; private set; }

    private void Awake() => PauseHandler.Add(this);

    private void Start()
    {
        StartCoroutine(ApplyTransitionsWithDelay());
        StartCoroutine(SetRandomValuesWithDelay());
    }

    private void Update()
    {
        if (OnPause)
            return;

        if (_movesAwayFromTheWall)
        {
            Vector3 direction = transform.parent.position - transform.position;

            Debug.DrawLine(transform.position, transform.parent.position);

            float angle = Vector3.Angle(direction, transform.forward);

            if (angle <= _angleTolerance)
            {
                _movesAwayFromTheWall = false;

                _targetValue = _valueForWalking;
                _transitionTargetValue = _transitionForWalking;

                _animator.SetFloat(_valueName, _valueForWalking);
                _animator.SetFloat(_transitionName, _transitionForWalking);
                
                StartCoroutine(StartMovingWithDelay());
            }
        }

        float currentTransitionValue = _animator.GetFloat(_transitionName);
        float currentValue = _animator.GetFloat(_valueName);

        float lerpedTransitionValue = Mathf.Lerp(currentTransitionValue, _transitionTargetValue, Time.deltaTime);
        float lerpedValue = Mathf.Lerp(currentValue, _targetValue, Time.deltaTime);

        _animator.SetFloat(_valueName, lerpedValue);
        _animator.SetFloat(_transitionName, lerpedTransitionValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        _movesAwayFromTheWall = true;
        
        StopAllCoroutines();

        _targetValue = _valueForRotation;
        _transitionTargetValue = _transitionForRotation;

        //_animator.SetFloat(_valueName, _valueForRotation);
        //_animator.SetFloat(_transitionName, _transitionForRotation);
    }

    private IEnumerator StartMovingWithDelay()
    {
        yield return new WaitForSeconds(Random.Range(_minTimeForMovesAwayFromTheWall, _maxTimeForMovesAwayFromTheWall));
        StartCoroutine(ApplyTransitionsWithDelay());
        StartCoroutine(SetRandomValuesWithDelay());
    }

    private IEnumerator ApplyTransitionsWithDelay()
    {
        yield return new WaitForSeconds(Random.Range(_minTransitionDelay, _maxTransitionDelay));
        _transitionTargetValue = Random.Range(_minTransitionValue, _maxTransitionValue);
        
        StartCoroutine(ApplyTransitionsWithDelay());
    }

    private IEnumerator SetRandomValuesWithDelay()
    {
        yield return new WaitForSeconds(Random.Range(_minDelay, _maxDelay));
        _targetValue = Random.Range(_minValue, _maxValue);
        
        StartCoroutine(SetRandomValuesWithDelay());
    }

    public void Pause() => OnPause = true;

    public void Play() => OnPause = false;
}
