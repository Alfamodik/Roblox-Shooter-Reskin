using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SFXProvider : MonoBehaviour
{
    private static SFXProvider _instance;
    
    [SerializeField, Range(0, 1)] private float _globalMusicVolume = 0.5f;
    [SerializeField, Range(0, 1)] private float _globalEffectsVolume = 0.5f;
    [SerializeField] private List<Sound> _sounds;

    private float _globalMusicVolumeBeforeMute;
    private float _globalEffectsVolumeBeforeMute;
    private bool _isMute;

    public static float GlobalMusicVolume
    {
        get => _instance._globalMusicVolume;
        set
        {
            _instance._globalMusicVolume = Mathf.Clamp01(value);
            _instance.UpdateSourcesVolume();
        }
    }

    public static float GlobalEffectsVolume
    {
        get => _instance._globalEffectsVolume;
        set
        {
            _instance._globalEffectsVolume = Mathf.Clamp01(value);
            _instance.UpdateSourcesVolume();
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        PlaySounds();

        YG2.onOpenAnyAdv += MuteAll;
        YG2.onCloseAnyAdv += UnmuteVolumeAfterCloseAdv;
    }

    private void OnDestroy()
    {
        YG2.onOpenAnyAdv -= MuteAll;
        YG2.onCloseAnyAdv -= UnmuteVolumeAfterCloseAdv;
    }

    public static void Play(string soundName)
        => _instance.PlayBySoundName(soundName);

    public static void UnmuteAll() 
        => _instance.UnmuteAllVolume();

    public static void MuteAll()
        => _instance.MuteAllVolume();

    [ContextMenu("UnmuteAll")]
    private void UnmuteAllVolume()
    {
        if (_isMute == false)
            return;

        Debug.Log("<color=#ffffff>UnmuteAllSFX</color>");
        _isMute = false;
        GlobalMusicVolume = _globalMusicVolumeBeforeMute;
        GlobalEffectsVolume = _globalEffectsVolumeBeforeMute;
    }

    private void UnmuteVolumeAfterCloseAdv()
    {
        if (_isMute == false)
            return;

        StartCoroutine(UnmuteAllVolumeAfterCloseAdv());
    }

    private IEnumerator UnmuteAllVolumeAfterCloseAdv()
    {
        Debug.Log("UnmuteAllVolumeAfterCloseAdv");
        yield return new WaitUntil(() => YG2.nowAdsShow == false);
        UnmuteAllVolume();
    }

    [ContextMenu("MuteAll")]
    private void MuteAllVolume()
    {
        if (_isMute)
            return;

        Debug.Log("<color=#ffffff>MuteAllSFX</color>");
        _isMute = true;
        _globalMusicVolumeBeforeMute = GlobalMusicVolume;
        _globalEffectsVolumeBeforeMute = GlobalEffectsVolume;

        GlobalMusicVolume = 0;
        GlobalEffectsVolume = 0;
    }

    private void PlayBySoundName(string soundName)
    {
        Sound sound = GetSound(soundName);

        if (sound == null)
        {
            Debug.LogError("Sound " + soundName + " not found!");
            return;
        }

        if (sound.Source == null)
        {
            InitializeSound(sound);
        }

        sound.Source.Stop();
        sound.Source.Play();
    }

    private void UpdateSourcesVolume()
    {
        foreach (var item in _sounds)
        {
            if (item.Source == null)
                continue;

            if (item.AudioType == AudioType.Music)
                item.Source.volume = item.Volume * _globalMusicVolume;
            else
                item.Source.volume = item.Volume * _globalEffectsVolume;
        }
    }

    private void PlaySounds()
    {
        foreach (Sound sound in _sounds)
        {
            if (sound.PlayOnAwake && !sound.AlreadyPlaying)
                Play(sound.Name);
        }
    }

    private void InitializeSound(Sound sound)
    {
        if (sound.DontDestroyOnLoad)
            sound.Source = gameObject.AddComponent<AudioSource>();
        else
            sound.Source = new GameObject(name = $"{sound.Name}").AddComponent<AudioSource>();

        if (sound.AudioType == AudioType.Music)
            sound.Source.volume = sound.Volume * GlobalMusicVolume;
        else
            sound.Source.volume = sound.Volume * GlobalEffectsVolume;

        sound.Source.clip = sound.Clip;
        sound.Source.loop = sound.Loop;
        sound.AlreadyPlaying = true;
    }

    private Sound GetSound(string name)
        => _sounds.Find(s => s.Name == name);

    [Serializable]
    public class Sound
    {
        public string Name;
        public bool Loop = false;
        public bool PlayOnAwake = false;
        public bool DontDestroyOnLoad = false;
        public AudioType AudioType;

        public AudioClip Clip;
        [Range(0, 1)] public float Volume = 1f;

        [HideInInspector] public bool AlreadyPlaying;
        [HideInInspector] public AudioSource Source;
    }

    public enum AudioType
    {
        Effects,
        Music
    }
}