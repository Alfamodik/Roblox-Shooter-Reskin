using System.Collections.Generic;
using UnityEngine;

public static class GlobalAudio
{
    private static List<AudioSource> _audioSources = new();

    public static void AddSource(AudioSource audioSource) => _audioSources.Add(audioSource);

    public static void RemoveSource(AudioSource audioSource) => _audioSources.Remove(audioSource);

    public static void SetMuteSuorces(bool active)
    {
        if(_audioSources.Count == 0)
            return;

        foreach(var item in _audioSources)
            item.mute = active;
    }

    public static void Dispose() => _audioSources = new List<AudioSource>();
}
