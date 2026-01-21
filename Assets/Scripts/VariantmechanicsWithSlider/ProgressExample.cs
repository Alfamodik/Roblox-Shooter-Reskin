using UnityEngine;

public class ProgressExample : MonoBehaviour
{
    [SerializeField] private SliderView _progressView;
    private Progress _progress;


    private void Awake()
    {
        _progress = new Progress(100, 100);

        _progressView.Initialize(_progress.Current, _progress.Max);

        _progress.Current.Changed += OnProgressChanged;

    }

    private void Oestroy()
    {
        _progress.Current.Changed -= OnProgressChanged;

    }

    private void OnProgressChanged(float arg1, float newValue)
    {
        if (newValue <= 0)
            Debug.Log("Progress is over");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            _progress.Add(10);

        if (Input.GetKeyDown(KeyCode.T))
            _progress.Reduce(10);

    }
}
