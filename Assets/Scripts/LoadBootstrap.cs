using UnityEngine;

public class LoadBootstrap : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _musicDuration;


    private void Awake()
    {
        if(_audioSource != null)
        {
            float audioVolume = _audioSource.volume;
            _audioSource.volume = 0;
            FadingOf.ChangeFading(_audioSource, audioVolume, _musicDuration);
        }

        if(_canvasGroup != null)
        {
            _canvasGroup.alpha = 1;
            FadingOf.ChangeFading(_canvasGroup, 0, _fadeDuration);
        }
    }
}
