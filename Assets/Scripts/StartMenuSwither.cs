using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuSwither : MonoBehaviour
{
    [SerializeField] private bool _activateOnStart = true;
    [SerializeField, Range(0, 0.5f)] private float _delayTime;

    [SerializeField] private PauseController _pauseController;

    [Space]
    [SerializeField] private List<GameObject> _startMenuUIElements = new();
    [SerializeField] private List<GameObject> _gameplayUIElements = new();

    [Space]
    [SerializeField] private Button _playButton;
    [SerializeField] private Camera _startMenuCamera;

    private void Start()
    {
        _playButton.onClick.AddListener(Deactivate);

        if(_activateOnStart)
            StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForSeconds(_delayTime);
        Activate();
    }

    public void Deactivate()
    {
        _startMenuUIElements.ForEach(e =>
        {
            e.SetActive(false);
        });

        _gameplayUIElements.ForEach(e =>
        {
            e.SetActive(true);
        });

        _pauseController.TakeOfPause(false);
        _startMenuCamera.gameObject.SetActive(false);
    }

    public void Activate()
    {
        _startMenuUIElements.ForEach(e =>
        {
            e.SetActive(true);
        });

        _gameplayUIElements.ForEach(e =>
        {
            e.SetActive(false);
        });

        _pauseController.SetPause(false);
        _startMenuCamera.gameObject.SetActive(false);
        _startMenuCamera.gameObject.SetActive(true);
    }
}
