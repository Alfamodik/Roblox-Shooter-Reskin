using System.Collections.Generic;

public static class PauseHandler
{
    private static List<IPauseble> _handlers = new();

    public static void Add(IPauseble handler) => _handlers.Add(handler);

    public static void Remove(IPauseble handler) => _handlers.Remove(handler);

    public static void SetPause(bool isPaused)
    {
        for(int i = 0; i < _handlers.Count; i++)
            _handlers[i].SetPause(isPaused);
    }

    public static void Dispose() => _handlers.Clear();
}
