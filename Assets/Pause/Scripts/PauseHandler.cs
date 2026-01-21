using System.Collections.Generic;

public static class PauseHandler
{
    private static readonly List<IPausable> _handlers = new();

    public static void Add(IPausable handler) => _handlers.Add(handler);

    public static void Remove(IPausable handler) => _handlers.Remove(handler);

    public static void Pause()
    {
        for(int i = 0; i < _handlers.Count; i++)
            _handlers[i].Pause();
    }

    public static void Play()
    {
        for(int i = 0; i < _handlers.Count; i++)
            _handlers[i].Play();
    }

    public static void Dispose() => _handlers.Clear();
}
