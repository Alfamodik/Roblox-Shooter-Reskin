using System;
using System.Collections.Generic;

public static class DisposeHandler
{
    private static List<IDisposable> _disposables = new();

    public static void Add(IDisposable disposable) => _disposables.Add(disposable);

    public static void DisposeAll()
    {
        FadingOf.Dispose();
        GlobalAudio.Dispose();
        PauseHandler.Dispose();

        foreach(IDisposable disposable in _disposables)
            disposable.Dispose();
        
        _disposables.Clear();
    }
}
