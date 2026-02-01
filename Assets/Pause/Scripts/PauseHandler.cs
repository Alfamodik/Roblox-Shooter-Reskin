using System.Collections.Generic;

public static class PauseHandler
{
    private static readonly List<IPausable> _handlers = new();
    private static bool _isPaused;

    public static bool IsPaused => _isPaused;

    public static void Add(IPausable handler) => _handlers.Add(handler);

    public static void Remove(IPausable handler) => _handlers.Remove(handler);

    public static void Pause()
    {
        _isPaused = true;
        for(int i = 0; i < _handlers.Count; i++)
            _handlers[i].Pause();
    }

    public static void Play()
    {
        _isPaused = false;
        for(int i = 0; i < _handlers.Count; i++)
            _handlers[i].Play();
    }

    public static void Dispose() => _handlers.Clear();
}
