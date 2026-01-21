public interface IPausable
{
    bool OnPause { get; }

    void Pause();

    void Play();
}
