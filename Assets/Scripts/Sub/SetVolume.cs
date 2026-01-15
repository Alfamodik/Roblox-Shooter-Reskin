using UnityEngine;

public class SetVolume : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private float _lastVolume;


    public void Mute()
    {
        _lastVolume = _audioSource.volume;
        _audioSource.volume = 0;
    }

    public void UnMute() => _audioSource.volume = _lastVolume;

    public void Change()
    {
        if(_audioSource.volume == 0)
            UnMute();
        else
            Mute();
    }
}
