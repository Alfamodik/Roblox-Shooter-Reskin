using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTranslition : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> _canvasGroups = new();
    [SerializeField] private List<AudioSource> _audioSources = new();
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _endValue;
    [SerializeField] private bool _startOnAwake;

    private void Start()
    {
        if(_startOnAwake == false)
            return;

        foreach(var item in _canvasGroups)
            FadingOf.ChangeFading(item, _endValue, _fadeDuration);
    }

    public void PlayButton()
        => StartCoroutine(Play());

    private IEnumerator Play()
    {
        foreach(var item in _canvasGroups)
            FadingOf.ChangeFading(item, _endValue, _fadeDuration);

        foreach(var item in _audioSources)
            FadingOf.ChangeFading(item, _endValue, _fadeDuration);

        yield return new WaitForSeconds(_fadeDuration);
        SceneLoader.LoadScene(SceneName.GamePlay);
    }
}
